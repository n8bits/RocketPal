using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RocketPal.Models.Game;

namespace RocketPal.Ai.Bots
{
    public class BlindBot : Bot
    {
        private IRocketLeagueInstance instance;
        private bool inControl = false;

        public void GiveControl(IRocketLeagueInstance instance)
        {
            this.instance = instance;
            this.inControl = true;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += this.SteerRandomly;
            worker.RunWorkerAsync();
        }

        private void SteerRandomly(object sender, DoWorkEventArgs arga)
        {
            var random = new Random();
            var steer = (float) random.Next(2, 10) / 10f;
            
            var throttle = (float)random.Next(5, 10) / 10f;

            while (inControl)
            {
                if (instance.Window.Focused)
                {
                    this.instance.Controller.Steer(steer);
                    this.instance.Controller.Throttle = throttle;
                    Thread.Sleep(2000);
                }
                else
                {
                    this.instance.Controller.Steer(0);
                    this.instance.Controller.Throttle = 0;
                    Thread.Sleep(2000);
                }
                Thread.Sleep(10);
            }
        }

        public void RevokeControl()
        {
            this.inControl = false;
        }
    }
}
