using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace RocketPal.Controller
{
    public class KeyboardSteerer : ISteerable
    {
        private BackgroundWorker steeringWorker;


        public KeyboardSteerer()
        {
            this.SteeringAmount = 0;
        }

        private void DoSteering(object sender, DoWorkEventArgs args)
        {
            var keyToPress = this.SteeringAmount < 0
                ? RocketLeagueKeyboardBindings.SteerLeft
                : RocketLeagueKeyboardBindings.SteerRight;
        }

        public Double SteeringAmount { get; }

        public void SteerLeft(double amount)
        {
            throw new NotImplementedException();
        }

        public void SteerRight(double ammount)
        {
            throw new NotImplementedException();
        }

        public void SteerCenter()
        {
            throw new NotImplementedException();
        }
    }
}
