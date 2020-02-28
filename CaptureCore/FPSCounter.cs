using System;
using System.Diagnostics;
using System.Timers;

namespace CaptureCore
{
    public class FPSCounter
    {
        private static System.Timers.Timer fpsTimer;
        private int frameCount = 0;
        private int lastFPS = 0;

        public FPSCounter()
        {
            fpsTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            fpsTimer.Elapsed += ResetFPS;
            fpsTimer.AutoReset = true;
            fpsTimer.Enabled = true;
        }

        public int GetFPS() => lastFPS;

        public void IncreaseFrame()
        {
            frameCount++;
        }

        private void ResetFPS(Object source, ElapsedEventArgs e)
        {
            lastFPS = frameCount;
            // Debug.WriteLine("FPS: {1}", "", lastFPS);
            frameCount = 0;
        }
    }
}
