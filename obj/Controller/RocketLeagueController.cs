using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using RocketPal.Memory;
using RocketPal.Models;

namespace RocketPal.Controller
{
    public class RocketLeagueController
    {
        public static readonly VirtualKeyCode AccelerateKey = VirtualKeyCode.VK_W;

        public static readonly VirtualKeyCode SteerLeftKey = VirtualKeyCode.VK_A;

        public static readonly VirtualKeyCode SteerRightKey = VirtualKeyCode.VK_D;

        public static readonly VirtualKeyCode PowerSlideKey = VirtualKeyCode.LSHIFT;
        

        private InputSimulator InputSimulator { get; }

        private BackgroundWorker steeringWorker;

        private BackgroundWorker throttleWorker;

        private BackgroundWorker jumpWorker;

        private float throttle = .6f;

        private float steering = 0;

        private float steeringPosition = 0;

        private bool boost = false;

        private bool jumpInProgress = false;

        public int RefreshRateMilliseconds = 20;


        public bool Connected = false;

        public RocketLeagueController()
        {
            this.InputSimulator = new InputSimulator();
            this.Connected = true;
            
        }
        
        public bool Boost
        {
            get
            {
                return this.boost;
            }

            set
            {
                if(!this.Boost && value)
                {
                    this.FireBoost(10);
                    this.boost = true;
                }else
                {
                    this.boost = value;
                }
            }
        }

        public void Jump()
        {
            this.InputSimulator.Mouse.RightButtonClick();
        }

        public void SetPowerSlide(bool slide)
        {
            if (slide)
            {
                this.InputSimulator.Keyboard.KeyDown(PowerSlideKey);
            }
            else
            {
                this.InputSimulator.Keyboard.KeyUp(PowerSlideKey);
            }
        }

        public void FireBoost(int milliseconds)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += this.BoostWorker;
            worker.RunWorkerAsync(milliseconds);
        }

        public void EnableSteeringAndThrottle()
        {
            throttleWorker = new BackgroundWorker();
            throttleWorker.DoWork += this.ThrottleWorker;
            throttleWorker.RunWorkerAsync();

            steeringWorker = new BackgroundWorker();
            steeringWorker.DoWork += this.SteeringWorker;
            steeringWorker.RunWorkerAsync();

            
        }

        public void SkipReplay()
        {
            this.InputSimulator.Mouse.RightButtonClick();
        }

        public void StartNewGame()
        {

            Thread.Sleep(30000);
            this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(1000);
            this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(20000);
            //Thread.Sleep(20000);
            //this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
            //Thread.Sleep(1000);

            //for (int i = 0; i < 6; i++)
            //{
            //    this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.UP);
            //    Thread.Sleep(1000);
            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            //    Thread.Sleep(1000);
            //}

            //this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            //this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
            //this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);

            //for (int i = 0; i < 6; i++)
            //{
            //    this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.UP);
            //    Thread.Sleep(1000);
            //}

            //this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            //Thread.Sleep(1000);

            //Thread.Sleep(1000);
            //this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            //Thread.Sleep(1000);
            //this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            //Thread.Sleep(1000);
            //this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);



        }

        public void FrontFlip()
        {
            if(!this.jumpInProgress)
            {
                jumpWorker = new BackgroundWorker();
                jumpWorker.DoWork += this.FrontFlipWorker;
                jumpWorker.RunWorkerAsync();
            }
               
        }

        public void FrontFlipWorker(object sender, DoWorkEventArgs args)
        {
            this.jumpInProgress = true;

            if (this.IsGameWindowFocused())
            {
                this.InputSimulator.Keyboard.KeyDown(AccelerateKey);
                this.InputSimulator.Mouse.RightButtonDoubleClick();
                Thread.Sleep(100);
                this.InputSimulator.Keyboard.KeyUp(AccelerateKey);
            }
            

            this.jumpInProgress = false;
        }

        public float Throttle
        {
            get { return this.throttle; }

            set
            {
                this.throttle = Math.Max(0, value);
                this.throttle = Math.Min(this.throttle, 1);
            }
        }

        public float SteeringPosition
        {
            get { return this.steeringPosition; }

            set
            {
                this.steeringPosition = Math.Max(-1, value);
                this.steeringPosition = Math.Min(this.steeringPosition, 1);
            }
        }

        private void BoostWorker(object sender, DoWorkEventArgs args)
        {
            if (this.IsGameWindowFocused() != true)
            {
                return;
            }
            this.InputSimulator.Mouse.LeftButtonDown();

            var milliseconds = (int) args.Argument;

            while(this.Boost && this.IsGameWindowFocused())
            {
                Thread.Sleep(milliseconds);
            }
            

            this.InputSimulator.Mouse.LeftButtonUp();
        }

        private void ThrottleWorker(Object sender, DoWorkEventArgs e)
        {
            while (this.Connected)
            {
                if (this.jumpInProgress)
                {
                    Thread.Sleep(500);
                    continue;
                }
                Thread.Sleep(10);
                WaitOnGameWindowsFocus();
                InputSimulator.Keyboard.KeyUp(AccelerateKey);
                //InputSimulator.Keyboard.KeyUp(PitchBackward);

                if (this.throttle > 0)
                {
                    int onTime = (int)(this.RefreshRateMilliseconds * this.throttle);
                    int offTime = this.RefreshRateMilliseconds - onTime;


                    InputSimulator.Keyboard.KeyDown(AccelerateKey);
                    InputSimulator.Keyboard.Sleep(onTime);
                        InputSimulator.Keyboard.KeyUp(AccelerateKey);
                        InputSimulator.Keyboard.Sleep(offTime);
                    
                }
            }
        }

        private void SteeringWorker(Object sender, DoWorkEventArgs e)
        {
            while (this.Connected)
            {
                Thread.Sleep(10);
                WaitOnGameWindowsFocus();
                InputSimulator.Keyboard.KeyUp(SteerRightKey);
                InputSimulator.Keyboard.KeyUp(SteerLeftKey);
                

                if (this.steeringPosition > 0)
                {
                    int onTime = (int)(this.RefreshRateMilliseconds * this.steeringPosition);
                    int offTime = this.RefreshRateMilliseconds - onTime;

                    InputSimulator.Keyboard.KeyDown(SteerRightKey);
                    InputSimulator.Keyboard.Sleep(onTime);
                    
                    InputSimulator.Keyboard.KeyUp(SteerRightKey);
                    InputSimulator.Keyboard.Sleep(offTime);
                    
                }
                else if (this.SteeringPosition < 0)
                {
                    int onTime = (int)(this.RefreshRateMilliseconds * Math.Abs(this.steeringPosition));
                    int offTime = this.RefreshRateMilliseconds - onTime;

                    InputSimulator.Keyboard.KeyDown(SteerLeftKey);
                    InputSimulator.Keyboard.Sleep(onTime);
                    InputSimulator.Keyboard.KeyUp(SteerLeftKey);
                    InputSimulator.Keyboard.Sleep(offTime);
                    
                }
            }
        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = MemoryEdits.GetForegroundWindow();

            if (MemoryEdits.GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public bool IsGameWindowFocused()
        {
            var activeWindow = this.GetActiveWindowTitle();

            while (activeWindow == null)
            {
                activeWindow = this.GetActiveWindowTitle();
            }

            return (activeWindow.Contains("Rocket League") && !activeWindow.Contains("Trainer"));
        }

        private void WaitOnGameWindowsFocus()
        {
            while (!this.IsGameWindowFocused())
            {
                Thread.Sleep(100);
            }
        }
    }
}
