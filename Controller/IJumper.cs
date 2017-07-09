using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Controller
{
    public interface IJumper
    {
        void SingleJump(TimeSpan buttonHoldDuration);

        void DoubleJump(TimeSpan firstJumpDuration, TimeSpan delayBetweenJumps, TimeSpan secondJumpDuration);
    }
}
