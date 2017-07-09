


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using RocketPal.Controller;

namespace RocketPal.Models.Menus
{
    public class MainMenu : INavigatableMenu
    {
        private static readonly int MaxMenuLevel = 10;

        private InputSimulator input;

        public IRocketLeagueController Controller { get; set; }

        public MainMenu(IRocketLeagueController controller)
        {
            Controller = controller;
            this.input = new InputSimulator();
        }

        public void GoToRoot()
        {
            for (int i = 0; i < MaxMenuLevel; i++)
            {
                this.Controller.GoBack();
            }
            this.input.Keyboard.Sleep(600);

            for (int i = 0; i < 20; i++)
            {
                this.input.Keyboard.KeyDown(VirtualKeyCode.UP);
                this.input.Keyboard.Sleep(30);
            }
            this.input.Keyboard.KeyUp(VirtualKeyCode.UP)
                ;
        }

        public void FindMatch()
        {
            this.GoToRoot();
            this.input.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            this.input.Keyboard.Sleep(400);
            this.input.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            this.input.Keyboard.Sleep(400);
            this.input.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            this.input.Keyboard.Sleep(400);
            this.input.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            this.input.Keyboard.Sleep(400);
        }

        public void UpOneLevel()
        {
            this.Controller.GoBack();
        }

        public void NavigateToItem(string itemName)
        {
            switch (itemName)
            {

                default:
                    throw new NotSupportedException($"Navigation to the menu item {itemName} is not supported!");
            }
        }
    }
}
