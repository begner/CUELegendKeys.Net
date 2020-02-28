//  ---------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// 
//  The MIT License (MIT)
// 
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
// 
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//  ---------------------------------------------------------------------------------

using System;
using Windows.Graphics.Capture;
using OpenCvSharp.Extensions;
using iCueSDK;
using LedResults;
using OpenCvSharp;
using OsHelper;
using System.Collections.Generic;
using Composition.WindowsRuntimeHelpers;
using System.Linq;
using System.Diagnostics;
using CaptureCore;

namespace CUELegendKeys
{
    public class ClientMap
    {
        public string ProcessType { get; private set; } = "";
        public IClientType Client { get; private set; }

        public ClientMap(string processType, IClientType client)
        {
            this.ProcessType = processType;
            this.Client = client;
        }
    }

    public class MainApplication : IDisposable
    {
        private ScreenCapture capture;

        private System.Windows.Controls.Image renderTarget = null;
        private ICueBridge iCueBridge;
        private readonly FPSCounter fpsCounter;
        private readonly ProcessDetection processDetection;

        private readonly IEnumerable<ClientMap> ClientMapping;

        private String GetCurrentClientTyp() {
            return processDetection.GetClientType();
        }

        private Rect GetCurrentProcessWindowRect()
        {
            Windows.Foundation.Rect pdRect = processDetection.GetWindowRect();
            Rect newRect = new Rect(Convert.ToInt32(pdRect.X), Convert.ToInt32(pdRect.Y), Convert.ToInt32(pdRect.Width), Convert.ToInt32(pdRect.Height));
            
            if (newRect.Width == 0)
            {
                newRect.Width = 1;
            }
            if (newRect.Height == 0)
            {
                newRect.Height = 1;
            }

            return newRect;
        }

        private bool frameWorkInProgress = false;

        public MainApplication(ProcessDetection processDetection, List<ClientMap> clientMapList)
        {
            this.processDetection = processDetection;
            this.ClientMapping = clientMapList.AsEnumerable();
            this.fpsCounter = new FPSCounter();
            
            IEnumerable<MonitorInfo> monitors = MonitorEnumerationHelper.GetMonitors();
            IEnumerable<MonitorInfo> primMons = from primaryMonitor in monitors
                                                where primaryMonitor.IsPrimary == true
                                                select primaryMonitor;
            MonitorInfo monitor = primMons.FirstOrDefault();
            GraphicsCaptureItem item = CaptureHelper.CreateItemForMonitor(monitor.Hmon);
            if (item != null)
            {
                this.StartCaptureFromItem(item);
            }
        }

        public void SetRenderTarget(System.Windows.Controls.Image image) => renderTarget = image;
        public void SetCueBridge(ICueBridge cb) => iCueBridge = cb;

        public int GetLastFPS()
        {
            return fpsCounter.GetFPS();
        }

        public void Dispose()
        {
            StopCapture();
        }

        public void FrameReady(object sender, EventArgs e)
        {
            if (!frameWorkInProgress)
            {
                frameWorkInProgress = true;

                if (renderTarget != null)
                {
                    var mat = capture.getLastFrameAsMat();
                    

                    if (mat == null)
                    {
                        return;
                    }

                    Rect appRect = this.GetCurrentProcessWindowRect();
                    try
                    {
                        mat = new Mat(mat, appRect);
                    }
                    catch (OpenCvSharp.OpenCVException ocve)
                    {
                        // Debug.WriteLine("OpenCvSharp.OpenCVException: {1}", "", ocve.Message);
                        // Debug.WriteLine("Rect: {1}/{2}/{3}/{4}", "", appRect.X, appRect.Y, appRect.Width, appRect.Height, ocve.Message);
                        // Debug.WriteLine("Mat: {1}/{2}", "", mat.Width, mat.Height);
                        mat = null;
                    }

                    ClientMap clientMap = (from ClientMap in this.ClientMapping where ClientMap.ProcessType == this.GetCurrentClientTyp() select ClientMap).FirstOrDefault();
                    IClientType client;
                    if (clientMap != null && mat != null)
                    {
                        client = clientMap.Client;
                        client.CaptureResult = mat;
                    }
                    else
                    {
                        client = new ClientTypeNone();
                    }

                    client.SetICueBridge(ref iCueBridge);
                    client.FPS = fpsCounter.GetFPS();

                    client.DoFrameAction();
                    renderTarget.Source = client.GetRenderTargetBitmapSource();

                    GC.Collect();
                    
                }
                frameWorkInProgress = false;
                fpsCounter.IncreaseFrame();
            }

        }

        public void StartCaptureFromItem(GraphicsCaptureItem item)
        {
            StopCapture();
            capture = new ScreenCapture(item);
            capture.FrameReady += FrameReady;
            capture.StartCapture();
        }

        public void StopCapture()
        {
            // capture.FrameReady -= FrameReady;
            capture?.Dispose();
        }
    }
}
