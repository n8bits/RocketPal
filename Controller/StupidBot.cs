using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace RocketPal.Controller
{
    public class StupidBot : IRocketLeagueDriver
    {
        //public KeyboardSimulator;

        public StupidBot()
        {
            this.Throttle.ThrottleAmount = 0;
            this.Steering.SteerCenter();
        }

        public ISteerable Steering { get; }
        public IThrottle Throttle { get; }

        public void SingleJump(TimeSpan buttonHoldDuration)
        {
            
        }

        public void DoubleJump(TimeSpan firstJumpDuration, TimeSpan delayBetweenJumps, TimeSpan secondJumpDuration)
        {
            throw new NotImplementedException();
        }

        public void StartBoost()
        {
            throw new NotImplementedException();
        }

        public void StopBoost()
        {
            throw new NotImplementedException();
        }
    }
}
