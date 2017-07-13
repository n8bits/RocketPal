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
using RocketPal.Models.Game;

namespace RocketPal.Controller
{
    public class KeyboardMouseController : IRocketLeagueController
    {
        public static readonly VirtualKeyCode AccelerateKey = VirtualKeyCode.VK_W;

        public static readonly VirtualKeyCode SteerLeftKey = VirtualKeyCode.VK_A;

        public static readonly VirtualKeyCode SteerRightKey = VirtualKeyCode.VK_D;

        public static readonly VirtualKeyCode PowerSlideKey = VirtualKeyCode.LSHIFT;

        protected InputSimulator InputSimulator { get; private set; }

        private BackgroundWorker steeringWorker;

        private BackgroundWorker throttleWorker;

        private BackgroundWorker jumpWorker;

        private float throttle = .6f;

        private float steering = 0;

        private double steeringPosition = 0;

        private bool boost = false;

        private bool jumpInProgress = false;

        public int RefreshRateMilliseconds = 1;


        private bool enabled ;

        public KeyboardMouseController()
        {
            InputSimulator dummy = new InputSimulator();
            this.InputSimulator = new InputSimulator(new AnalogKeyboardSimulator(dummy), new MouseSimulator(dummy), new WindowsInputDeviceStateAdaptor());
            this.enabled = false;
        }

        private AnalogKeyboardSimulator AnalogKeyboardSimulator => this.InputSimulator.Keyboard as AnalogKeyboardSimulator;

        private void initControls()
        {
            List<VirtualKeyCode> resetKeys = new List<VirtualKeyCode>()
            {
                RocketLeagueKeyboardBindings.ThrottleForward,
                RocketLeagueKeyboardBindings.SteerLeft,
                RocketLeagueKeyboardBindings.SteerRight,
                RocketLeagueKeyboardBindings.ThrottleReverse,
                RocketLeagueKeyboardBindings.Handbrake,
                RocketLeagueKeyboardBindings.RollLeft,
                RocketLeagueKeyboardBindings.RollRight,
                RocketLeagueKeyboardBindings.PitchDown,
                RocketLeagueKeyboardBindings.PitchUp
            };
            foreach (var rocketLeagueKeyboardBinding in resetKeys)
            {
                AnalogKeyboardSimulator.AnalogKeyUp(rocketLeagueKeyboardBinding);
            }
        }

        public void ToggleInGameMenu()
        {
            if (!this.Enabled)
                return;

            this.InputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
        }

        public void GoBack()
        {
            if (!this.Enabled)
                return;
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
                    if (!this.Enabled)
                        return;
                    this.InputSimulator.Mouse.MouseButtonDown(RocketLeagueKeyboardBindings.Boost);
                }
                else
                {
                    this.InputSimulator.Mouse.MouseButtonUp(RocketLeagueKeyboardBindings.Boost);
                }
            }
        }

        public bool Enabled
        {
            get { return enabled && GameWindow.Focused; }

            set
            {
                if (!value && this.enabled)
                {
                    initControls();
                    Console.WriteLine("Disabling controls...Done");
                }else if (value && !this.enabled)
                {
                    this.enabled = value;
                    this.Throttle = this.throttle;
                    this.Steer(this.steeringPosition);
                }
                this.enabled = value;
            }
        }

        public void Jump()
        {
            if (!this.Enabled)
                return;
            this.InputSimulator.Mouse.MouseButtonClick(RocketLeagueKeyboardBindings.Jump);
        }

        public void HandBrakeEngaged(bool slide)
        {
            if (!this.Enabled)
                return;

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
            if (!this.Enabled)
                return;
        }

        public void SkipReplay()
        {
            if (!this.Enabled)
                return;
            this.InputSimulator.Mouse.RightButtonClick();
        }

        public void FrontFlip()
        {
            if (!this.Enabled)
                return;
            this.Jump();
            this.AnalogKeyboardSimulator.KeyDown(RocketLeagueKeyboardBindings.PitchDown);
            this.Jump();
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
                if (!this.Enabled)
                    return;
                this.AnalogKeyboardSimulator.AnalogKeyDown(keyToPush, Math.Abs(this.throttle));
            }
        }

        public float SteeringPosition => (float) this.steeringPosition;

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
            //Console.WriteLine("Steering " + amount);
            this.steeringPosition = amount;
            this.steeringPosition = Math.Max(-1, amount);
            this.steeringPosition = Math.Min(this.steeringPosition, 1);
            if (!this.Enabled)
                return;

            if (Math.Abs(amount) < 0.1d)
            {
                this.AnalogKeyboardSimulator.AnalogKeyUp(RocketLeagueKeyboardBindings.SteerRight);
                this.AnalogKeyboardSimulator.AnalogKeyUp(RocketLeagueKeyboardBindings.SteerLeft);
            }
            else if (amount < 0)
            {
                this.AnalogKeyboardSimulator.AnalogKeyUp(RocketLeagueKeyboardBindings.SteerRight);
                this.AnalogKeyboardSimulator.AnalogKeyDown(RocketLeagueKeyboardBindings.SteerLeft, Math.Abs(amount));
                this.steeringPosition = amount;
            }
            else
            {
                this.AnalogKeyboardSimulator.AnalogKeyUp(RocketLeagueKeyboardBindings.SteerLeft);
                this.AnalogKeyboardSimulator.AnalogKeyDown(RocketLeagueKeyboardBindings.SteerRight, amount);
                this.steeringPosition = amount;
            }
        }
    }
}
