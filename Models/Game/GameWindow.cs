using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RocketPal.Memory;

namespace RocketPal.Models.Game
{
    public class GameWindow
    {
        public static bool Focused
        {
            get
            {
                var activeWindow = Title;

                while (activeWindow == null)
                {
                    activeWindow = Title;
                    //Thread.Sleep(10);
                }

                return (activeWindow.Contains("Rocket League") && !activeWindow.Contains("Trainer"));
            }          
        }
        private static string Title
        {
            get
            {
                const int nChars = 256;
                var Buff = new StringBuilder(nChars);
                IntPtr handle = MemoryEdits.GetForegroundWindow();

                return MemoryEdits.GetWindowText(handle, Buff, nChars) > 0 ? Buff.ToString() : null;
            }
           
        }

        public static void WaitForFocus()
        {
            while (!GameWindow.Focused)
            {
                Thread.Sleep(1);
            }
        }

        public static void BringToForeground()
        {
            var WINDOW_HANDLER = MemoryEdits.FindWindow(null, @"Rocket League (32-bit, DX9)");
            if (WINDOW_HANDLER == 0)
            { }
            else
            {
                MemoryEdits.SetForegroundWindow((IntPtr) WINDOW_HANDLER);
                MemoryEdits.ShowWindow(WINDOW_HANDLER, 9);
            }
        }
    }
}
