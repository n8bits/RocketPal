using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Controller
{
    public interface IThrottle
    {
        bool Reverse { get; set; }

        double ThrottleAmount { get; set; }

        void Kill();
    }
}
