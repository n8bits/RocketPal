using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Controller;
using RocketPal.Models.Menus;
using System.Windows.Forms;

namespace RocketPal.Models.Game
{
    public interface IRocketLeagueInstance
    {

        GameWindow Window { get; }

        Menus.MainMenu MainMenu { get; }

        IRocketLeagueController Controller { get; set; }

        bool InGame { get; }

        void SearchForMatch(TimeSpan timeout, bool continuousSearch = true);
    }
}
