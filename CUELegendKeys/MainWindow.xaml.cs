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

using CaptureCore;
using Composition.WindowsRuntimeHelpers;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Windows.Foundation.Metadata;
using Windows.Graphics.Capture;
using Windows.UI.Composition;
using OsHelper;
using System.Collections.Generic;
// using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using OsHelper;
using iCueSDK;

namespace CUELegendKeys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainApplication mainApplication;

        public MainWindow()
        {
            InitializeComponent();
            this.Left = 2000;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            // Process Watcher
            List<ProcessItem> processItems = new List<ProcessItem>(new ProcessItem[] {
                new ProcessItem("gameClient", "League of Legends"),
                new ProcessItem("launcher", "LeagueClientUx")
            });
            var processDetection = new ProcessDetection(processItems);

            ClientTypeGame gameClient = new ClientTypeGame();
            gameClient.previewImageSkillQRenderTarget = previewImageSkillQ;
            gameClient.previewImageSkillWRenderTarget = previewImageSkillW;
            gameClient.previewImageSkillERenderTarget = previewImageSkillE;
            gameClient.previewImageSkillRRenderTarget = previewImageSkillR;
            gameClient.previewImageSkillDRenderTarget = previewImageSkillD;
            gameClient.previewImageSkillFRenderTarget = previewImageSkillF;

            gameClient.previewImageItem1RenderTarget = previewImageItem1;
            gameClient.previewImageItem2RenderTarget = previewImageItem2;
            gameClient.previewImageItem3RenderTarget = previewImageItem3;
            gameClient.previewImageItem4RenderTarget = previewImageItem4;
            gameClient.previewImageItem5RenderTarget = previewImageItem5;
            gameClient.previewImageItem6RenderTarget = previewImageItem6;
            gameClient.previewImageItem7RenderTarget = previewImageItem7;

            gameClient.previewCharImageRenderTarget = previewCharImage;

            List<ClientMap> clientMapList = new List<ClientMap>(new ClientMap[] {
                   new ClientMap("gameClient", gameClient),
                   new ClientMap("launcher", new ClientTypeLauncher()),
            });

            
            mainApplication = new MainApplication(processDetection, clientMapList);
            mainApplication.previewCaptureImageRenderTarget = previewCaptureImage;


            // Cue Connection
            var cb = new ICueBridge();
            mainApplication.setiCueBridge(ref cb);

        }
    }
}

