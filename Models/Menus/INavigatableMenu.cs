using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Controller;

namespace RocketPal.Models.Menus
{
    interface INavigatableMenu
    {
        void GoToRoot();

        void UpOneLevel();

        void NavigateToItem(string itemName);
    }
}
