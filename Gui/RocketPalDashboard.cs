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
using RocketPal.Ai.Bots;
using RocketPal.Models.Game;
using RocketPal.Models.GameElements;

namespace RocketPal.Gui
{
    public partial class RocketPalDashboard : Form
    {
        private DefaultRocketLeagueInstance instance;

        public RocketPalDashboard()
        {
            InitializeComponent();
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => {
                this.instance = DefaultRocketLeagueInstance.GetDefaultRocketLeagueInstance();
                this.Status.Text = "Default Instance Aquired.";
                this.instance.ClockInfoPanel = this.ClockSearchProgress;
                
                this.Status.Text = "Beginning Search...";
                instance.SearchForMatch(TimeSpan.FromSeconds(120));

                
            }));
           
        }

        private void FindClockButton_Click(object sender, EventArgs e)
        {
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += this.LocateClock;
            //worker.RunWorkerAsync();
        }

        private void OnClockLocated(object sender, RunWorkerCompletedEventArgs args)
        {
            this.Invoke(new Action(() =>
            {
                this.Status.Text = this.instance.StatusMessage;
            }));
        }

        //private void LocateClock(object sender, DoWorkEventArgs args)
        //{
        //    DateTime expiration = DateTime.Now.AddSeconds(120);
        //    var instance = DefaultRocketLeagueInstance.GetDefaultRocketLeagueInstance();
        //   instance.ClockInfoPanel = this.ClockSearchProgress;

        //    GameClock clock = null;

        //    while (DateTime.Now < expiration && clock == null)
        //    {
        //        clock = this.instance.Clock;
        //    }

        //    if (clock != null)
        //    {
        //        instance.CurrentMatch = new Match(clock);
        //        instance.CurrentMatch.WatchClock();
        //    }
        //}

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.instance.Controller.Throttle = (float) trackBar1.Value/10f;
        }

        private void SteeringSlider_ValueChanged(object sender, EventArgs e)
        {
            this.instance.Controller.Steer(((float) SteeringSlider.Value)/10f);
        }
    }
}
