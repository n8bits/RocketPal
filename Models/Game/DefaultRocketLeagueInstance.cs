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

        public GameClock CurrentClock { get; private set; }

        public GameClock FindClock()
        {
            var foundAddresses = MemoryScanner.FindSignatureInMemory(GameClock.signature, false, null, ClockInfoPanel);
            if (foundAddresses.Any())
            {
                Console.WriteLine("Game Clock found at " + foundAddresses);
                return new GameClock(foundAddresses);
            }
            else return null;
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
            //var windowWatcher = new BackgroundWorker();
            //windowWatcher.DoWork += this.MonitorWindowFocus;
            //windowWatcher.RunWorkerAsync();
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
            Console.WriteLine(this.StatusMessage);

            this.Window.BringToForeground();

            if (GameWindow.Focused)
            {
                this.StatusMessage = "Bringing window to foreground...";
                Console.WriteLine(this.StatusMessage);

                BackgroundWorker worker = new BackgroundWorker();

                worker.DoWork += this.PlayAMatch;
                worker.RunWorkerAsync(DateTime.Now.Add(timeout));
                worker.RunWorkerCompleted += this.RoundCompletedTask;
            }
            else
            {
                this.StatusMessage = "Bringing window to foreground...Done";
                Console.WriteLine(this.StatusMessage);
            }
        }

        /// <summary>
        /// Push the "Find Match" button and begin searching game memory for game objects. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args" >Expiration DateTime after which the search should be canceled.</param>
        private void PlayAMatch(object sender, DoWorkEventArgs args)
        {
            DateTime expiration = (DateTime)args.Argument;

            this.MainMenu.FindMatch();
            this.ControlBot.GiveControl(this);

            // Search for clock until expiration runs out
            //while (DateTime.Now < expiration && this.CurrentClock == null)
            while (this.CurrentClock == null)
            {
                this.CurrentClock = this.FindClock();
                if (this.CurrentClock.TimeRemaining > 1000 || CurrentClock.TimeRemaining == 0)
                {
                    this.CurrentClock = null;
                }
                this.StatusMessage = "Waiting for match to be found....giving up in " +
                                     expiration.Subtract(DateTime.Now).Seconds;
                Console.WriteLine(this.StatusMessage);
                Thread.Sleep(100);
            }

            this.StatusMessage = "Clock found.";
            this.CurrentMatch = new Match(this.CurrentClock);
            this.Controller.GoBack();

            while (this.CurrentMatch.MatchComplete != true)
            {
                Thread.Sleep(100);
            }

            this.StatusMessage = "Match is complete.";
        }

        private void MonitorWindowFocus(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                Thread.Sleep(10);
                this.Controller.Enabled = GameWindow.Focused;
            }
        }

        private void RoundCompletedTask(object sender, AsyncCompletedEventArgs args)
        {

            this.StatusMessage = "Round has concluded. Revoking controls...";
            Console.WriteLine(this.StatusMessage);
            this.ControlBot.RevokeControl();
            this.StatusMessage = this.StatusMessage + "Done";
            Console.WriteLine(this.StatusMessage);
            Thread.Sleep(10000);
            if (true)
            {
                this.StatusMessage = "Continuous search is on, begining next match..";
                Console.WriteLine(this.StatusMessage);

                this.SearchForMatch(TimeSpan.FromMinutes(3));
            }
        }

        public bool ContinuousPlay { get; set; }
    }
}
