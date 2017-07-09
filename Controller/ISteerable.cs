using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Controller
{
    public interface ISteerable
    {
        void SteerLeft(double amount);

        void SteerRight(double ammount);

        void SteerCenter();
    }
}
