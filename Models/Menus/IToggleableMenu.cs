using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Models.Menus
{
    interface IToggleableMenu
    {
        void ToggleVisible();
    }
}
