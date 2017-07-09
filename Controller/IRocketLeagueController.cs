using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Controller
{
    public interface IRocketLeagueController
    {
        bool BoostEngaged { get; set; }

        float Throttle { get; set; }

        void Jump();

        void ToggleInGameMenu();

        void HandBrakeEngaged(bool slide);

        void SkipReplay();

        void GoBack();

        void FrontFlip();
        
        void Steer(double amount);
    }
}
