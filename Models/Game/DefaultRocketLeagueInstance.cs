using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RocketPal.Ai.Bots;
using RocketPal.Components;
using RocketPal.Controller;
using RocketPal.Models.GameElements;
using RocketPal.Models.Menus;
using System.Windows.Forms;

namespace RocketPal.Models.Game
{
    public class DefaultRocketLeagueInstance : IRocketLeagueInstance
    {

        
        private KeyboardMouseController pcControls;

        private readonly Match currentMatch;

        private BackgroundWorker clockLocatorWorker;

        public Bot ControlBot;

        public Menus.MainMenu MainMenu
        {
            get
            {
                return new Menus.MainMenu(this.Controller);
            }
        }

        public Match CurrentMatch { get; set; }

        public MemoryScanInfoPanel ClockInfoPanel { get; set; }

        public string Status { get; set; }

        public bool InGame => this.currentMatch == null;

        public static DefaultRocketLeagueInstance GetDefaultRocketLeagueInstance()
        {
                return new DefaultRocketLeagueInstance();
        }

        public static DefaultRocketLeagueInstance OpenNewInstance()
        {
            var executable = ConfigurationManager.AppSettings.Get("RlExecutablePath");
            ProcessStartInfo info = new ProcessStartInfo(executable);
            var process = Process.Start(info);
            
            return GetDefaultRocketLeagueInstance();
        }

        private DefaultRocketLeagueInstance()
        {
            this.pcControls = new KeyboardMouseController();
            ControlBot = new BlindBot();
            this.clockLocatorWorker = new BackgroundWorker();
            this.clockLocatorWorker.WorkerReportsProgress = true;
        }

        public KeyboardMouseController PcControls => this.pcControls;

        public GameWindow Window => new GameWindow();

        public IRocketLeagueController Controller
        {
            get
            {
                return this.pcControls; 
                
            }

            set { this.pcControls = value as KeyboardMouseController; }
        }
        
        public void SetMatchmakingPlaylists(IList<MatchmakingPlaylists.RankedPlaylists> rankedPlaylists)
        {
            
        }

        public void SetMatchmakingPlaylist(IList<MatchmakingPlaylists.UnrankedPlaylists> unrankedPlaylists)
        {
            throw new NotImplementedException();
        }

        public void SetRegions(IList<MatchmakingPlaylists.Regions> regions)
        {
            throw new NotImplementedException();
        }

        public void SearchForMatch(TimeSpan timeout, bool continuousPlay = true)
        {
            if (this.Status != null)
            {
                this.Status = "Bringing window to foreground...";
            }
            this.Window.BringToForeground();
            
            if (this.Window.Focused)
            {
                if (this.Status != null)
                {
                    this.Status = "Bringing window to foreground...Success";
                }
                BackgroundWorker worker = new BackgroundWorker();

                worker.DoWork += this.BeginMatchmakingSearch;
                worker.RunWorkerCompleted += this.RoundCompletedTask;
                worker.RunWorkerAsync(DateTime.Now.Add(timeout));
            }
            else
            {
                if (this.Status != null)
                {
                    this.Status = "Bringing window to foreground...Failed";
                }
            }
        }

        public async void SearchForClock()
        {
            if (this.clockLocatorWorker.IsBusy)
            {
                return;
            }
            else
            {
                
                this.clockLocatorWorker.DoWork += (object sender, DoWorkEventArgs args) =>
                {
                    DateTime expiration = DateTime.Now.AddSeconds(120);
                    var instance = DefaultRocketLeagueInstance.GetDefaultRocketLeagueInstance();

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
                };
            }
            
        }

        private void BeginMatchmakingSearch(object sender, DoWorkEventArgs args)
        {
            DateTime expiration = (DateTime) args.Argument;

            this.MainMenu.FindMatch();
            GameClock clock = null;

            while (DateTime.Now < expiration && clock == null)
            {
                 clock = GameClock.GetClock(this.ClockInfoPanel);
            }

            if (clock != null)
            {
                this.CurrentMatch = new Match(clock);
                this.CurrentMatch.WatchClock();
                this.ControlBot.GiveControl(this);

                while (currentMatch.MatchComplete != true)
                {
                    Thread.Sleep(100);
                }

                this.ControlBot.RevokeControl();
            }
        }

        private void RoundCompletedTask(object sender, AsyncCompletedEventArgs args)
        {
            if (this.ContinuousPlay)
            {
                this.SearchForMatch(TimeSpan.FromMinutes(3));
            }
        }

        public bool ContinuousPlay { get; set; }
    }
}
