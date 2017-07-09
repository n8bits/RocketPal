using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Models.Game;

namespace RocketPal.Models.Menus
{
    public interface IRocketLeagueMenu
    {
        void SetMatchmakingPlaylists(IList<MatchmakingPlaylists.RankedPlaylists> rankedPlaylists);

        void SetMatchmakingPlaylist(IList<MatchmakingPlaylists.UnrankedPlaylists> unrankedPlaylists);

        void SetRegions(IList<MatchmakingPlaylists.Regions> regions);
    }
}
