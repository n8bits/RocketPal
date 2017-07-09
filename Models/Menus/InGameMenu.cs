using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Controller;

namespace RocketPal.Models.Menus
{
    public class InGameMenu : IToggleableMenu, INavigatableMenu
    {
        private IRocketLeagueController controller;

        public InGameMenu(IRocketLeagueController controller)
        {
            this.controller = controller;
        }
        
        public void ToggleVisible()
        {
            controller.ToggleInGameMenu();
        }

        public void GoToRoot()
        {
            throw new NotImplementedException();
        }

        public void UpOneLevel()
        {
            throw new NotImplementedException();
        }

        public void NavigateToItem(string itemName)
        {
            throw new NotImplementedException();
        }
    }
}
