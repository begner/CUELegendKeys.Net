using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OsHelper
{
    public class ProcessItem
    {
        public string ProcessType { get; private set; } = "";
        public string ProcessName { get; private set; } = "";
        public Process Process { get; set; }

        public ProcessItem(string processType, string processName)
        {
            this.ProcessType = processType;
            this.ProcessName = processName;
            this.Process = null;
        }
    }

    public class ProcessDetection : IDisposable
    {
        private readonly List<ProcessItem> processItems;
        private readonly ForegroundProcessListener foregroundProcessListener;

        private ProcessItem activeProcessItem;

        public string GetClientType()
        {
            if (this.activeProcessItem != null)
            {
                return this.activeProcessItem.ProcessType;
            }
            return "";
        }

        public Windows.Foundation.Rect GetWindowRect()
        {
            Windows.Foundation.Rect pRect = new Windows.Foundation.Rect();
            if (this.activeProcessItem.Process.MainWindowHandle != null)
            {
                pRect = WindowEnumerationHelper.GetWindowRectXY(this.activeProcessItem.Process.MainWindowHandle);
                // Debug.WriteLine("process: {1}/{2} {3}x{4}", "", activeProcess.MainWindowHandle, activeProcess.MainWindowTitle, pRect.X, pRect.Y);
            }
            return pRect;
        }

        public ProcessDetection(List<ProcessItem> processItems)
        {
            this.processItems = processItems;
            this.foregroundProcessListener = new ForegroundProcessListener();
            this.foregroundProcessListener.Callback += new ForegroundProcessListener.CallbackEventHandler(OnProcessChanged);
            this.foregroundProcessListener.initializeEventListener();
            this.OnProcessChanged((IntPtr)null);
        }

        public void Dispose()
        {
            this.foregroundProcessListener.Callback -= new ForegroundProcessListener.CallbackEventHandler(OnProcessChanged);
        }

        public void OnProcessChanged(IntPtr hwnd)
        {
            ProcessItem pItem = GetCaptureableProcess();
            if (pItem != null && (this.activeProcessItem == null || this.activeProcessItem.ProcessName != pItem.ProcessName))
            {
                Debug.WriteLine("onProcessChanged: {1} / {2}", "", pItem.ProcessType, pItem.Process.MainWindowHandle, pItem.Process.MainWindowTitle);
                this.activeProcessItem = pItem;
            }
            else
            {
                // this.activeProcessItem = null;
            }
        }

        private ProcessItem GetCaptureableProcess()
        {
            foreach (ProcessItem pItem in processItems)
            {
                var ps = Process.GetProcessesByName(pItem.ProcessName);

                if (ps.Length > 0)
                {
                    Process process = ps[0];
                    // if (WindowEnumerationHelper.IsWindowValidForCapture(process.MainWindowHandle))
                    // {
                        // Debug.WriteLine("process: {1}/{2}/{3}", "", pName, process.MainWindowHandle, process.MainWindowTitle);
                        pItem.Process = process;
                        return pItem;
                    // }
                }
            }
            return null;
        }
    }
}

