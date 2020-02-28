using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OsHelper
{
    public class ProcessDetection : IDisposable
    {
        private readonly List<string> autoCaptureProcessNames;
        private readonly ForegroundProcessListener foregroundProcessListener;

        private Process activeProcess;

        public string GetClientType()
        {
            return "game";
        }

        public Windows.Foundation.Rect GetWindowRect()
        {
            Windows.Foundation.Rect pRect = new Windows.Foundation.Rect();
            if (activeProcess != null)
            {
                pRect = WindowEnumerationHelper.GetWindowRectXY(activeProcess.MainWindowHandle);
                // Debug.WriteLine("process: {1}/{2} {3}x{4}", "", activeProcess.MainWindowHandle, activeProcess.MainWindowTitle, pRect.X, pRect.Y);
            }
            return pRect;
        }

        public ProcessDetection(List<string> titlesToCapture)
        {
            autoCaptureProcessNames = titlesToCapture;

            foregroundProcessListener = new ForegroundProcessListener();
            foregroundProcessListener.Callback += new ForegroundProcessListener.CallbackEventHandler(OnProcessChanged);
            foregroundProcessListener.initializeEventListener();
        }

        public void Dispose()
        {
            foregroundProcessListener.Callback -= new ForegroundProcessListener.CallbackEventHandler(OnProcessChanged);
        }

        public void OnProcessChanged(IntPtr hwnd)
        {
            Process process = GetCaptureableProcess();
            if (process != null)
            {
                if (activeProcess == null || activeProcess.ProcessName != process.ProcessName)
                {
                    Debug.WriteLine("onProcessChanged: {1}/{2}", "", process.MainWindowHandle, process.MainWindowTitle);
                    activeProcess = process;
                }
            }
        }

        private Process GetCaptureableProcess()
        {
            foreach (string pName in autoCaptureProcessNames)
            {
                var ps = Process.GetProcessesByName(pName);

                if (ps.Length > 0)
                {
                    Process process = ps[0];
                    if (WindowEnumerationHelper.IsWindowValidForCapture(process.MainWindowHandle))
                    {
                        // Debug.WriteLine("process: {1}/{2}/{3}", "", pName, process.MainWindowHandle, process.MainWindowTitle);
                        return process;
                    }
                }
            }
            return null;
        }
    }
}

