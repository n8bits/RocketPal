using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RocketPal.Models.Game;
using static Probability.Lottery;

namespace RocketPal.Ai.Bots
{
    public class BlindBot : Bot
    {
        private IRocketLeagueInstance instance;
        private bool inControl = false;
        private double steerIncrement = 0.1d;

        public void GiveControl(IRocketLeagueInstance instance)
        {
            this.instance = instance;
            this.inControl = true;
            this.instance.Controller.Enabled = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += this.SteerRandomly;
            worker.RunWorkerAsync();
        }

        private void SteerRandomly(object sender, DoWorkEventArgs arga)
        {
            this.instance.Controller.Steer(0.9d);
            while (inControl)
            {
                GameWindow.WaitForFocus();
                this.instance.Controller.Enabled = true;

                if (PlayLottery(0.025d))
                {
                    this.instance.Controller.Jump();

                    if (PlayLottery(0.5d))
                    {
                        //Thread.Sleep(100);
                        this.instance.Controller.Jump();
                    }
                }

                //if (PlayLottery(0.50d))
                //{
                //    this.steerIncrement-= 0.1d;
                //}
                //else
                //{
                //    this.steerIncrement+= 0.1d;
                //}

                //if (PlayLottery(0.5d))
                //{
                //    this.instance.Controller.Steer(this.instance.Controller.SteeringPosition + steerIncrement);
                //}

                if (PlayLottery(0.1d))
                {
                    this.instance.Controller.Steer(this.instance.Controller.SteeringPosition * -1d );
                }

                if (PlayLottery(0.1d))
                {
                    var prev = this.instance.Controller.SteeringPosition;
                    this.instance.Controller.Steer(0);
                    if(PlayLottery(0.1d))
                    this.steerIncrement = 0d;
                    Thread.Sleep(3000);
                    this.instance.Controller.Steer(prev);
                }

                if (PlayLottery(0.05d))
                {
                    this.instance.Controller.Throttle = 0;
                    Thread.Sleep(2000);
                }
                else
                {
                    this.instance.Controller.Throttle = 0.9f;
                }
                Thread.Sleep(500);
            }

            (sender as BackgroundWorker).Dispose();
        }

        public void RevokeControl()
        {
            this.inControl = false;
        }
    }
}
