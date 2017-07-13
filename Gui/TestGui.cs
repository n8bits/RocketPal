using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RocketPal.Models.Game;
using RocketPal.Models.GameElements;
using RocketPal.Models.GameObjects;

namespace RocketPal.Gui
{
    public partial class TestGui : Form
    {
        private IRocketLeagueInstance instance;

        public TestGui()
        {
            InitializeComponent();
            instance = DefaultRocketLeagueInstance.GetDefaultRocketLeagueInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (true)
            {
                if (GameWindow.Focused)
                {
                    this.instance.Controller.Steer(.2);
                    this.instance.Controller.Throttle = .9f;
                    Thread.Sleep(2000);
                }
                else
                {
                    this.instance.Controller.Steer(0);
                    Thread.Sleep(2000);
                }
                Thread.Sleep(10);
            }
           
        }

        private void GoToRootButton_Click(object sender, EventArgs e)
        {
            instance.Window.BringToForeground();

            if (GameWindow.Focused)
            {
                instance.MainMenu.FindMatch();
            }
        }

        private void LaunchNewInstanceButton_Click(object sender, EventArgs e)
        {
            this.instance = DefaultRocketLeagueInstance.OpenNewInstance();
        }

        private void FindCarButton_Click(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += this.ClockWatcher;
            worker.RunWorkerAsync();
        }

        private void ClockWatcher(Object sender, DoWorkEventArgs args)
        {
            var clock = instance.CurrentClock;

            while (clock.TimeRemaining > 0)
            {
                Console.WriteLine("Clock:  " + clock.TimeRemaining);
                Thread.Sleep(400);
            }
        }


    }
}
