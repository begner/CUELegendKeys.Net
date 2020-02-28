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

using Composition.WindowsRuntimeHelpers;
using OpenCvSharp;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using Windows.Graphics;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.UI.Composition;

namespace CaptureCore
{
    public class ScreenCapture : IDisposable
    {
        private GraphicsCaptureItem item;
        private Direct3D11CaptureFramePool framePool;
        private GraphicsCaptureSession session;
        private SizeInt32 lastSize;

        private IDirect3DDevice device;
        private SharpDX.Direct3D11.Device d3dDevice;
        private SwapChain1 swapChain;

        private Texture2D lastFrame;
        private Mat lastMat;

        public Mat getLastFrameAsMat()
        {
            return lastMat;
        }

        public event EventHandler FrameReady;

        protected virtual void OnFrameReady(EventArgs e)
        {
            FrameReady?.Invoke(this, e);
        }

        public ScreenCapture(GraphicsCaptureItem i)
        {
            item = i;
            
            device = Direct3D11Helper.CreateDevice(); 
            d3dDevice = Direct3D11Helper.CreateSharpDXDevice(device);

            SizeInt32 correctedSize = item.Size;
            correctedSize.Width = correctedSize.Width / 8 * 8;
            correctedSize.Height = correctedSize.Height / 8 * 8;

            var dxgiFactory = new Factory2();
            var description = new SwapChainDescription1()
            {
                Width = correctedSize.Width, 
                Height = correctedSize.Height,
                Format = Format.B8G8R8A8_UNorm,
                Stereo = false,
                SampleDescription = new SampleDescription()
                {
                    Count = 1,
                    Quality = 0
                },
                Usage = Usage.RenderTargetOutput,
                BufferCount = 2,
                Scaling = Scaling.Stretch,
                SwapEffect = SwapEffect.FlipSequential,
                AlphaMode = AlphaMode.Premultiplied,
                Flags = SwapChainFlags.None
            };
            swapChain = new SwapChain1(dxgiFactory, d3dDevice, ref description);

            framePool = Direct3D11CaptureFramePool.Create(
                device,
                DirectXPixelFormat.B8G8R8A8UIntNormalized,
                2,
                correctedSize);
            session = framePool.CreateCaptureSession(i);
            lastSize = correctedSize;

            framePool.FrameArrived += OnFrameArrived;
        }

        public void Dispose()
        {
            session?.Dispose();
            framePool?.Dispose();
            swapChain?.Dispose();
            d3dDevice?.Dispose();
            device?.Dispose();
        }

        public void StartCapture()
        {
            session.StartCapture();
        }

        public ICompositionSurface CreateSurface(Compositor compositor)
        {
            return compositor.CreateCompositionSurfaceForSwapChain(swapChain);
        }

        private void OnFrameArrived(Direct3D11CaptureFramePool sender, object args)
        {
            var newSize = false;

            using (var frame = sender.TryGetNextFrame())
            {
                if (frame.ContentSize.Width != lastSize.Width ||
                    frame.ContentSize.Height != lastSize.Height)
                {
                    // The thing we have been capturing has changed size.
                    // We need to resize the swap chain first, then blit the pixels.
                    // After we do that, retire the frame and then recreate the frame pool.
                    newSize = true;
                    lastSize = frame.ContentSize;
                    lastSize.Width = lastSize.Width / 8 * 8;
                    lastSize.Height = lastSize.Height / 8 * 8;
                    swapChain.ResizeBuffers(
                        2, 
                        lastSize.Width, 
                        lastSize.Height, 
                        Format.B8G8R8A8_UNorm, 
                        SwapChainFlags.None);
                }
                     
                using (var backBuffer = swapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0))
                using (var bitmap = Direct3D11Helper.CreateSharpDXTexture2D(frame.Surface))
                {
                    d3dDevice.ImmediateContext.CopyResource(bitmap, backBuffer);

                    if (lastFrame != null)
                    {
                        d3dDevice.ImmediateContext.UnmapSubresource(lastFrame, 0);
                        lastFrame.Dispose();
                    }

                    // Create texture copy
                    lastFrame = new SharpDX.Direct3D11.Texture2D(d3dDevice, new SharpDX.Direct3D11.Texture2DDescription
                    {
                        Width = bitmap.Description.Width,
                        Height = bitmap.Description.Height,
                        MipLevels = 1,
                        ArraySize = 1,
                        Format = Format.B8G8R8A8_UNorm,
                        Usage = ResourceUsage.Staging,
                        SampleDescription = new SampleDescription(1, 0),
                        BindFlags = BindFlags.None,
                        CpuAccessFlags = CpuAccessFlags.Read,
                        OptionFlags = ResourceOptionFlags.None
                    });
                    d3dDevice.ImmediateContext.CopyResource(bitmap, lastFrame);

                    var dataBox = d3dDevice.ImmediateContext.MapSubresource(lastFrame, 0, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None, out DataStream stream);
                    var rect = new DataRectangle
                    {
                        DataPointer = stream.DataPointer,
                        Pitch = dataBox.RowPitch
                    };
                    lastMat = new Mat(lastFrame.Description.Height, lastFrame.Description.Width, MatType.CV_8UC4, stream.DataPointer); // width % 4 != 0

                    OnFrameReady(EventArgs.Empty);
                }
            } // Retire the frame.

            // swapChain.Present(0, SharpDX.DXGI.PresentFlags.None);
            if (newSize)
            {
                framePool.Recreate(
                    device,
                    DirectXPixelFormat.B8G8R8A8UIntNormalized,
                    2,
                    lastSize);
            }
        }
    }
}
