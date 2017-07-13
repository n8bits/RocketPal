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
using RocketPal.Memory;

namespace RocketPal.Models.Game
{
    public class DefaultRocketLeagueInstance : IRocketLeagueInstance
    {
        private KeyboardMouseController pcControls;

        private readonly Match currentMatch;

        private BackgroundWorker clockWorker;

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
        
        public string StatusMessage { get; set; }

        public bool InGame => this.currentMatch == null;

        public GameClock Clock
        {
            get
            {
                var foundAddresses = MemoryScanner.FindSignatureInMemory(GameClock.signature, false, null, ClockInfoPanel);
                if (foundAddresses.Any())
                {
                    Console.WriteLine("Game Clock found at " + foundAddresses);
                    return new GameClock(foundAddresses);
                }
                else return null;
            }
        }
        
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
            this.clockWorker = new BackgroundWorker();
            this.clockWorker.WorkerReportsProgress = true;
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

        public void SearchForMatch(TimeSpan timeout, bool startNewMatchWhenFinished = true)
        {
            this.StatusMessage = "Bringing window to foreground...";

            this.Window.BringToForeground();
            
            if (this.Window.Focused)
            {
                this.StatusMessage = "Bringing window to foreground...";

                BackgroundWorker worker = new BackgroundWorker();

                worker.DoWork += this.DoMatchmakingSearch;
                worker.RunWorkerCompleted += this.RoundCompletedTask;
                worker.RunWorkerAsync(DateTime.Now.Add(timeout));
            }
            else
            {
                this.StatusMessage = "Bringing window to foreground...Done";
            }
        }

        public async void SearchForClock()
        {
            if (this.clockWorker.IsBusy || this.InGame)
            {
                return;
            }
            else
            {
                this.clockWorker.DoWork += (object sender, DoWorkEventArgs args) =>
                {
                    DateTime expiration = DateTime.Now.AddSeconds(120);
                    var instance = DefaultRocketLeagueInstance.GetDefaultRocketLeagueInstance();

                    GameClock clock = null;

                    while (DateTime.Now < expiration && clock == null)
                    {
                        clock = this.Clock;
                    }

                    if (clock != null)
                    {
                        instance.CurrentMatch = new Match(clock);
                        instance.CurrentMatch.WatchClock();
                    }
                };
            }
        }

         /// <summary>
         /// Push the "Find Match" button and begin searching game memory for game objects. 
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="args" >Expiration DateTime after which the search should be canceled.</param>
        private void DoMatchmakingSearch(object sender, DoWorkEventArgs args)
        {
            DateTime expiration = (DateTime) args.Argument;

            this.MainMenu.FindMatch();

            while (DateTime.Now < expiration && this.Clock == null)
            {
                this.SearchForClock();
                this.StatusMessage = "Waiting for match to be found....giving up in " +
                                     expiration.Subtract(DateTime.Now).Seconds;
                Thread.Sleep(100);
            }
            
            this.CurrentMatch = new Match(Clock);
            this.CurrentMatch.WatchClock();
            this.ControlBot.GiveControl(this);

            while (currentMatch.MatchComplete != true)
            {
                Thread.Sleep(100);
            }

            this.ControlBot.RevokeControl();
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
