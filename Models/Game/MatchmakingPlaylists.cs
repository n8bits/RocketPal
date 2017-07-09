using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Models.Game
{
    public class MatchmakingPlaylists
    {
        public enum UnrankedPlaylists
        {
            Duel,
            Doubles,
            Standard,
            Chaos,
            SnowDay,
            RocketLabs,
            Hoops,
            Rumble,
            Dropshot
        }

        public enum RankedPlaylists
        {
            SoloDuel,
            Doubles,
            SoloStandard,
            Standard
        }

        public enum Regions
        {
            AsiaMainland,
            Europe,
            AsiaEast,
            MiddleEast,
            Oceania,
            SouthAmerica,
            UsEast,
            UsWest
        }


    }
}
