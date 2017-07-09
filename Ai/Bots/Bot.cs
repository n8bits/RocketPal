using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Models.Game;
using RocketPal.Models.GameElements;
using RocketPal.Models.GameObjects;

namespace RocketPal.Ai.Bots
{
    public interface Bot
    {
        void GiveControl(IRocketLeagueInstance instance);

        void RevokeControl();
    }
}
