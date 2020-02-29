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

        public System.Windows.Controls.Image previewCaptureImageRenderTarget { get; set; } = null;
        
        private readonly FPSCounter fpsCounter;
        private readonly ProcessDetection processDetection;
        private readonly IEnumerable<ClientMap> ClientMapping;
               

        private ICueBridge ICueBridge;
        public void setiCueBridge(ref ICueBridge iCueBridge)
        {
            this.ICueBridge = iCueBridge;
        }

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

        private bool frameAnalysisInProgress = false;


        private Mat mockImage = null;

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


            switch (Settings.AppRunMode)
            {
                case SettingsAppRunMode.MockGame:
                    this.mockImage = new Mat(Settings.MockImageGame);
                    break;
                case SettingsAppRunMode.MockLauncher:
                    this.mockImage = new Mat(Settings.MockImageLauncher);
                    break;
            }

            if (item != null)
            {
                this.StartCaptureFromItem(item);
            }
        }


        
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
            try
            {
                if (!frameAnalysisInProgress)
                {
                    frameAnalysisInProgress = true;

                    IClientType client = null;
                    ClientMap clientMap = null;

                    // Capture Screen 
                    switch (Settings.AppRunMode)
                    {
                        case SettingsAppRunMode.Normal:

                            // Capture Image
                            Mat capturedImage = capture.getLastFrameAsMat();
                            if (capturedImage == null)
                            {
                                frameAnalysisInProgress = false;
                                return;
                            }

                            // Cut out current Window
                            Rect appRect = this.GetCurrentProcessWindowRect();
                            try
                            {
                                capturedImage = new Mat(capturedImage, appRect);
                            }
                            catch (OpenCvSharp.OpenCVException ocve)
                            {
                                // Debug.WriteLine("OpenCvSharp.OpenCVException: {1}", "", ocve.Message);
                                // Debug.WriteLine("Rect: {1}/{2}/{3}/{4}", "", appRect.X, appRect.Y, appRect.Width, appRect.Height, ocve.Message);
                                // Debug.WriteLine("Mat: {1}/{2}", "", mat.Width, mat.Height);
                                capturedImage = null;
                            }

                            // Detect Client Type
                            clientMap = (from ClientMap in this.ClientMapping where ClientMap.ProcessType == this.GetCurrentClientTyp() select ClientMap).FirstOrDefault();
                            if (clientMap != null && capturedImage != null)
                            {
                                client = clientMap.Client;
                                client.CaptureResult = capturedImage;
                            }
                            else
                            {
                                client = new ClientTypeNone();
                            }
                            break;

                        case SettingsAppRunMode.MockLauncher:
                            clientMap = (from ClientMap in this.ClientMapping where ClientMap.ProcessType == "launcher" select ClientMap).FirstOrDefault();
                            client = clientMap.Client;
                            client.CaptureResult = this.mockImage;
                            break;

                        case SettingsAppRunMode.MockGame:
                            clientMap = (from ClientMap in this.ClientMapping where ClientMap.ProcessType == "gameClient" select ClientMap).FirstOrDefault();
                            client = clientMap.Client;
                            client.CaptureResult = this.mockImage;
                            break;
                    }

                    // client.SetICueBridge(ref iCueBridge);
                    client.FPS = fpsCounter.GetFPS();
                    client.SetICueBridge(ref this.ICueBridge);

                    client.DoFrameAction();
                    previewCaptureImageRenderTarget.Source = client.GetRenderTargetBitmapSource();

                    GC.Collect();

                    fpsCounter.IncreaseFrame();
                }
            }
            catch(Exception myException)
            {
                frameAnalysisInProgress = false;
            }
            finally {
                frameAnalysisInProgress = false;
            }

        }

        System.Windows.Threading.DispatcherTimer mockTimer = new System.Windows.Threading.DispatcherTimer();
        public void StartCaptureFromItem(GraphicsCaptureItem item)
        {
            if (Settings.AppRunMode == SettingsAppRunMode.Normal)
            {
                this.StopCapture();
                capture = new ScreenCapture(item);
                capture.FrameReady += FrameReady;
                capture.StartCapture();
            }
            else
            {
                // Hook up the Elapsed event for the timer. 
                mockTimer.Tick += new EventHandler(FrameReady);
                mockTimer.Interval = new TimeSpan(0, 0, 1);
                mockTimer.Start();
            }
        }
            
        public void StopCapture()
        {
            if (Settings.AppRunMode == SettingsAppRunMode.Normal)
            {
                // capture.FrameReady -= FrameReady;
                capture?.Dispose();
            }
            else
            {
                mockTimer.Stop();
            }
        }
    }
}
