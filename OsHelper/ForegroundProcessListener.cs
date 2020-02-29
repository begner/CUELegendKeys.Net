using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace OsHelper
{
    class ForegroundProcessListener
    {
        public delegate void CallbackEventHandler(IntPtr hwnd);
        public event CallbackEventHandler Callback;

        private static WinEventDelegate winEventProc;
        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
        
        public void initializeEventListener()
        {
            winEventProc = new WinEventDelegate(WinEventProc);
            IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, winEventProc, 0, 0, WINEVENT_OUTOFCONTEXT);
        }
        
        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            // Debug.WriteLine("Window Changed: {0} ({1})", "", WindowEnumerationHelper.GetForegroundWindow());
            if (Callback != null)
            {
                Callback(hwnd);
            }
        }
    }
}
