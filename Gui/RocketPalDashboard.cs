using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RocketPal.Ai.Bots;
using RocketPal.Models.Game;
using RocketPal.Models.GameElements;

namespace RocketPal.Gui
{
    public partial class RocketPalDashboard : Form
    {
        private IRocketLeagueInstance instance;

        public RocketPalDashboard()
        {
            InitializeComponent();
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => {
                var instance = DefaultRocketLeagueInstance.GetDefaultRocketLeagueInstance();
                this.Status.Text = "Default Instance Aquired.";
                instance.ClockInfoPanel = this.ClockSearchProgress;
                this.Status.Text = "Beginning Search...";
                instance.SearchForMatch(TimeSpan.FromSeconds(120));
                this.Status.Text = "Search worker started.";
            }));
           
        }

        private void FindClockButton_Click(object sender, EventArgs e)
        {
            
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += this.LocateClock;
            worker.RunWorkerAsync();
        }

        private void OnClockLocated(object sender, RunWorkerCompletedEventArgs args)
        {
            this.Invoke(new Action(() =>
            {
                this.Status.Text = this.instance.ga
            }));
        }

        private void LocateClock(object sender, DoWorkEventArgs args)
        {
            DateTime expiration = DateTime.Now.AddSeconds(120);
            var instance = DefaultRocketLeagueInstance.GetDefaultRocketLeagueInstance();
           instance.ClockInfoPanel = this.ClockSearchProgress;

            GameClock clock = null;

            while (DateTime.Now < expiration && clock == null)
            {
                clock = GameClock.GetClock(this.ClockSearchProgress);
            }

            if (clock != null)
            {
                instance.CurrentMatch = new Match(clock);
                instance.CurrentMatch.WatchClock();
            }
        }
        
    }
}
