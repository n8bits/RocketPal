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
using RocketPal.Extensions;
using RocketPal.Memory;
using RocketPal.Models;

namespace RocketPal.Controller
{
    public class KeyboardMouseController : IRocketLeagueController
    {
        public static readonly VirtualKeyCode AccelerateKey = VirtualKeyCode.VK_W;

        public static readonly VirtualKeyCode SteerLeftKey = VirtualKeyCode.VK_A;

        public static readonly VirtualKeyCode SteerRightKey = VirtualKeyCode.VK_D;

        public static readonly VirtualKeyCode PowerSlideKey = VirtualKeyCode.LSHIFT;
        
        public InputSimulator InputSimulator { get; }

        private BackgroundWorker steeringWorker;

        private BackgroundWorker throttleWorker;

        private BackgroundWorker jumpWorker;

        private float throttle = .6f;

        private float steering = 0;

        private double steeringPosition = 0;

        private bool boost = false;

        private bool jumpInProgress = false;

        public int RefreshRateMilliseconds = 20;

        public bool Connected = false;

        public KeyboardMouseController()
        {
            InputSimulator dummy = new InputSimulator();
            this.InputSimulator = new InputSimulator(new AnalogKeyboardSimulator(dummy), new MouseSimulator(dummy), new WindowsInputDeviceStateAdaptor());
            this.Connected = true;
        }

        private AnalogKeyboardSimulator AnalogKeyboardSimulator => this.InputSimulator.Keyboard as AnalogKeyboardSimulator;

        public void ToggleInGameMenu()
        {
            this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        }

        public void GoBack()
        {
            this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        }

        public bool BoostEngaged
        {
            get { return this.boost; }
            set
            {
                this.boost = value;

                if (this.boost)
                {
                    this.InputSimulator.Mouse.MouseButtonDown(RocketLeagueKeyboardBindings.Boost);
                }
                else
                {
                    this.InputSimulator.Mouse.MouseButtonUp(RocketLeagueKeyboardBindings.Boost);
                }
            }
        }

        public void Jump()
        {
            this.InputSimulator.Mouse.MouseButtonClick(RocketLeagueKeyboardBindings.Jump);
        }

        public void HandBrakeEngaged(bool slide)
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
        }

        public void SkipReplay()
        {
            this.InputSimulator.Mouse.RightButtonClick();
        }
        
        public void FrontFlip()
        {
            
        }
        
        public float Throttle
        {
            get { return this.throttle; }

            set
            {
                this.throttle = Math.Max(-1, value);
                this.throttle = Math.Min(this.throttle, 1);
                var keyToPush = throttle < 0
                    ? RocketLeagueKeyboardBindings.ThrottleReverse
                    : RocketLeagueKeyboardBindings.ThrottleForward;
                this.AnalogKeyboardSimulator.AnalogKeyDown(keyToPush, this.throttle);
            }
        }

        public double SteeringPosition => this.steeringPosition;
        
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
        
        public void Steer(double amount)
        {
            if (Math.Abs(amount) < 0.1d)
            {
                this.AnalogKeyboardSimulator.AnalogKeyUp(RocketLeagueKeyboardBindings.SteerRight);
                this.AnalogKeyboardSimulator.AnalogKeyUp(RocketLeagueKeyboardBindings.SteerLeft);
            }
            else if (amount < 0)
            {
                this.AnalogKeyboardSimulator.AnalogKeyDown(RocketLeagueKeyboardBindings.SteerLeft, amount);
                this.steeringPosition = amount;
            }
            else
            {
                this.AnalogKeyboardSimulator.AnalogKeyDown(RocketLeagueKeyboardBindings.SteerRight, amount);
            }
        }
    }
}
