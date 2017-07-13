using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using RocketPal.Extensions;
using RocketPal.Models.Game;

namespace RocketPal.Controller
{
    public class AnalogKeyboardSimulator : KeyboardSimulator
    {
        public static readonly double DefaultTolerance = 0.000001d;

        public double PwmIntervalNanoseconds;

        private BackgroundWorker pwmWorker;

        private ConcurrentDictionary<VirtualKeyCode, BackgroundWorker> currentlyDownKeys;

        private ConcurrentDictionary<VirtualKeyCode, double> currentlyDownAmounts;

        public AnalogKeyboardSimulator(IInputSimulator inputSimulator) : base(inputSimulator)
        {
            this.currentlyDownKeys = new ConcurrentDictionary<VirtualKeyCode, BackgroundWorker>();
            this.currentlyDownAmounts = new ConcurrentDictionary<VirtualKeyCode, double>();
            this.pwmWorker = new BackgroundWorker();

            PwmIntervalNanoseconds = 10000;
        }

        public void AnalogKeyDown(VirtualKeyCode key, double amount)
        {
            amount = amount > 1 ? 1 : amount; // normalize to max value of 1

            if (amount < 0)
            {
                amount = 0;
            }

            // Return if the new amount is the same as the current amount
            if (Math.Abs(this.GetDownAmount(key) - amount) < DefaultTolerance)
            {
                return;
            }

            // If the key should be continuously held down, just call KeyDown and dont waste time
            // using a worker thread
            if (amount >= 1)
            {
                this.KeyDown(key);
                this.currentlyDownAmounts[key] = 1d;
                return;
            }

            //
            this.updateKeyDownAmount(key, amount);
        }

        public void AnalogKeyUp(VirtualKeyCode keyCode)
        {
            this.currentlyDownAmounts[keyCode] = 0;

            BackgroundWorker worker = null;

            if (this.currentlyDownKeys.TryGetValue(keyCode, out worker))
            {
                worker.CancelAsync();
                
            }
            this.KeyUp(keyCode);
        }

        public double GetDownAmount(VirtualKeyCode keyCode)
        {
            return this.currentlyDownAmounts.GetOrAdd(keyCode, 0);
        }

        private void updateKeyDownAmount(VirtualKeyCode keyCode, double amount)
        {
            this.currentlyDownAmounts[keyCode] = amount;

            BackgroundWorker worker = null;

            if (this.currentlyDownKeys.TryGetValue(keyCode, out worker))
            {
                worker.CancelAsync();
                while (worker.IsBusy)
                {
                    Console.WriteLine("Worker is still busy");
                    Thread.Sleep(1);
                }
                this.currentlyDownKeys[keyCode] = worker;
                this.currentlyDownKeys[keyCode].RunWorkerAsync(new object[] { keyCode, amount });
            }
            else
            {
                worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += this.DoAnalogKeyDown;
                worker.RunWorkerCompleted += this.KeyPresserWorkComplete;
                this.currentlyDownKeys[keyCode] = worker;
                this.currentlyDownKeys[keyCode].RunWorkerAsync(new object[] { keyCode, amount });
            }

            
        }

        public bool IsKeyDown(VirtualKeyCode key)
        {
            BackgroundWorker worker = null;

            return this.currentlyDownKeys.TryGetValue(key, out worker);
        }

        private void DoAnalogKeyDown(object sender, DoWorkEventArgs args)
        {
            var watch = new Stopwatch();
            var parameters = args.Argument;
            var worker = sender as BackgroundWorker;
            var key = (VirtualKeyCode)((Object[])parameters)[0];
            var amount = (double)((Object[])parameters)[1];
            var onTime = TimeSpan.FromTicks(StopwatchExtensions.NanoSecondsToTicks((long) (this.PwmIntervalNanoseconds * amount)));
            var offTime = TimeSpan.FromTicks(StopwatchExtensions.NanoSecondsToTicks((long)(this.PwmIntervalNanoseconds))).Subtract(onTime);
            args.Result = key;

            while (!worker.CancellationPending)
            {
                GameWindow.WaitForFocus();
                this.KeyDown(key);

                Thread.Sleep(onTime);

                this.KeyUp(key);

                Thread.Sleep(offTime);
            }
            args.Cancel = true;
        }

        private void KeyPresserWorkComplete(object sender, RunWorkerCompletedEventArgs args)
        {

        }
    }
}
