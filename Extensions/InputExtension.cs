using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace RocketPal.Extensions
{
    public static class InputExtensions
    {
        public static void MouseButtonDown(this IMouseSimulator mouse, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    mouse.LeftButtonDown();
                    break;

                case MouseButton.RightButton:
                    mouse.RightButtonDown();
                    break;

                default:
                    break;

            }
        }

        public static void MouseButtonUp(this IMouseSimulator mouse, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    mouse.LeftButtonUp();
                    break;

                case MouseButton.RightButton:
                    mouse.RightButtonUp();
                    break;

                default:
                    break;
            }
        }

        public static void MouseButtonClick(this IMouseSimulator mouse, MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    mouse.LeftButtonClick();
                    break;

                case MouseButton.RightButton:
                    mouse.RightButtonClick();
                    break;

                default:
                    break;
            }
        }
    }
}
