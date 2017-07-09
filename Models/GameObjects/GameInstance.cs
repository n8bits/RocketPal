using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RocketPal.Controller;
using RocketPal.Memory;
using RocketPal.Models.GameElements;
using Timer = System.Windows.Forms.Timer;
using RocketPal.Components;

namespace RocketPal.Models.GameObjects
{
    public class GameInstance
    {
        public Car PlayerCar { get; set; }

        public Ball Ball { get; set; }

        public GameClock Clock { get; set; }

        public BoostMeter Boost { get; set; }

        public List<int> IgnoredBallAddresses = new List<int>();

        public List<int> IgnoredCarAddresses = new List<int>();

        public Timer timer;

        public Timer staleObjectsChecker;

        public TextBox Console { get; set; }

        private BackgroundWorker carGetter, ballGetter, replaySkipper, clockGetter, diligentSearcher;

        public AutoPilot ap;

        private float lastClock = 400f;

        private float lastX = 0f;

        private float lastBallX = 0f;

        private bool goalScored = false;

        private bool SkipReplayInProgress = false;

        private bool overtime = false;

        public bool gameOver = false;

        public ProgressBar BallProgressBar = null;

        public MemoryScanInfoPanel CarInfoPanel = null;

        public MemoryScanInfoPanel BallInfoPanel = null;

        public MemoryScanInfoPanel ClockInfoPanel = null;

        private RocketLeagueKeyboardController _menuKeyboardController;

        public GameInstance()
        {
        }

        public void Start()
        {
            timer = new Timer();
            staleObjectsChecker = new Timer();
            this.Clock = null;
            ap = new AutoPilot(this);

            _menuKeyboardController = new RocketLeagueKeyboardController();
            clockGetter = new BackgroundWorker();
            clockGetter.DoWork += this.LocateClock;
            clockGetter.RunWorkerAsync();

            //BackgroundWorker boostGetter = new BackgroundWorker();
            //boostGetter.DoWork += this.LocateBoost;
            //boostGetter.RunWorkerAsync();

            carGetter = new BackgroundWorker();
            carGetter.DoWork += this.LocateCar;
            carGetter.RunWorkerAsync();

            ballGetter = new BackgroundWorker();
            ballGetter.DoWork += this.LocateBall;
            ballGetter.RunWorkerAsync();

            diligentSearcher = new BackgroundWorker();
            diligentSearcher.DoWork += this.DiligentMemorySearcherTask;
            // diligentSearcher.RunWorkerAsync();

            this.timer.Enabled = true;
            timer.Tick += new EventHandler(onTick);
            timer.Interval = 1100;

            this.staleObjectsChecker.Enabled = true;
            staleObjectsChecker.Tick += new EventHandler(this.CheckStaleObjects);
            staleObjectsChecker.Interval = 3500;

            
            ap.Engage();

        }

        public bool GameReady
        {
            get { return this.PlayerCar != null &&  this.Ball != null; }
        }

        public bool BallFound()
        {
            return this.Ball != null;
        }

        public bool CarFound()
        {
            return this.PlayerCar != null;
        }

        public void LocateClock(Object sender, DoWorkEventArgs args)
        {
            while (this.Clock == null)
            {
                this.Clock = GameClock.GetClock(this.ClockInfoPanel);
            }
            
            Thread.Sleep(1000);
        }

        public void LocateBoost(Object sender, DoWorkEventArgs args)
        {
            while (this.Boost == null)
            {
                this.Boost = BoostMeter.GetBoostMeter();
            }

            Thread.Sleep(1000);
        }

        public void LogMessage(string message)
        {
            if (this.Console != null)
            {
                this.Console.TopLevelControl.BeginInvoke(new MemoryScanInfoPanel.InvokeDelegate(() =>
                {
                    this.Console.AppendText(message + "\n");
                    this.Console.TopLevelControl.Refresh();
                }));
                
            }
        }

        public void onTick(object sender, EventArgs e)
        {
            var clockRundown = 2f;

            if (this.Clock != null)
            {
                // If the clock timer runs out
                if (Clock.TimeRemaining < 1)
                {
                    this.LogMessage("Time up, waiting to see if it's overtime or not.....");
                    // this.overtime will be set true if the clock starts going back up
                    WaitToSeeIfOvertimeStarts();

                    if (this.overtime)
                    {
                        this.LogMessage("Overtime detected!");
                    }
                    else
                    {
                        this.LogMessage("Game over! Attempting to start matchmaking search...");
                        this._menuKeyboardController.StartNewGame();
                        this.LogMessage("Attempt complete. MatchmakingPlaylists should be underway now.");
                        this.gameOver = true;
                        this.Clock = null;
                        return;
                    }
                    
                }

                clockRundown = this.lastClock - Clock.TimeRemaining;
                
                if (clockRundown > 1 && goalScored)
                {
                    goalScored = false;
                    this.SkipReplayInProgress = false;
                   
                }
                
            }

            this.CheckClockStopped(clockRundown);

            if (this.overtime && Math.Abs(clockRundown) < 1)
            {
                LogMessage("Overtime ended, Game over!");
                this.gameOver = true;
                this.Clock = null;
                return;
            }

            if (Clock != null)
            {
                lastClock = this.Clock.TimeRemaining;
            }
        }

        public void WaitToSeeIfOvertimeStarts()
        {
            Thread.Sleep(15000);

            if (this.Clock.TimeRemaining >= 1)
            {
                this.overtime = true;
                LogMessage("Overtime detected!");
            }
        }

        public void CheckClockStopped(float clockRundown)
        {
            if (this.CarFound())
            {
                if (clockRundown < 1 && clockRundown >= 0)
                {
                    this.PlayerCar = null;
                    this.Ball = null;
                    goalScored = true;
                    this.LogMessage("Goal!!!! Resetting...");
                    this.SkipReplay();
                    FetchBall();
                    FetchCar();
                }
            }
        }

        public bool IsGameWindowFocused()
        {
            var activeWindow = this.GetActiveWindowTitle();

            while (activeWindow == null)
            {
                activeWindow = this.GetActiveWindowTitle();
            }

            return (activeWindow.Contains("Rocket League") && !activeWindow.Contains("Trainer"));
        }
        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = MemoryEdits.GetForegroundWindow();

            if (MemoryEdits.GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public void CheckStaleObjects(object sender, EventArgs e)
        {
            if (this.CarFound())
            {
                if (Math.Abs(this.PlayerCar.X - lastX) < 1 && this.IsGameWindowFocused())
                {
                    this.IgnoredCarAddresses.Add(this.PlayerCar.MemoryAddress);
                    this.PlayerCar = null;
                    this.ap.AvoidIdle();
                    this.FetchCar();
                }
                else
                {
                    this.lastX = this.PlayerCar.X;
                }
            }

            if (this.BallFound())
            {
                if (Math.Abs(this.Ball.X - lastBallX) < 1 && this.IsGameWindowFocused())
                {
                    this.IgnoredBallAddresses.Add(this.Ball.MemoryAddress);
                    this.Ball = null;
                    this.FetchBall();
                }
                else
                {
                    this.lastBallX = this.Ball.X;
                }
            }
            
            if (!this.GameReady)
            {
                this.ap.DriveBlindlyUntilBallIsFound();
            }
            
        }

        public void FetchBall()
        {
            ballGetter = new BackgroundWorker();
            ballGetter.DoWork += this.LocateBall;
            ballGetter.RunWorkerAsync();
        }

        public void FetchCar()
        {
            carGetter = new BackgroundWorker();
            carGetter.DoWork += this.LocateCar;
            carGetter.RunWorkerAsync();
        }

        public void SkipReplay()
        {
            if(this.SkipReplayInProgress)
            {
                return;
            }

            replaySkipper = new BackgroundWorker();
            replaySkipper.DoWork += this.SkipReplayTask;
            replaySkipper.RunWorkerAsync();
        }

        public void SkipReplayTask(Object sender, DoWorkEventArgs args)
        {
            this.SkipReplayInProgress = true;
            LogMessage("Skipping replay...");
            for(int i=0; i< 5; i++)
            {
                Thread.Sleep(1000);
                this._menuKeyboardController.SkipReplay();
            }

            LogMessage("Skip replay done.");

            ap.DriveBlindlyUntilBallIsFound();
        }

        
        
        public void DiligentMemorySearcherTask(object sender, DoWorkEventArgs args)
        {
            //var ballAddresses = MemoryScanner.FindSignatureInMemory(Ball.signature, true);
            //var carAddresses = MemoryScanner.FindSignatureInMemory(Car.signature, true);

        }

        public void reset()
        {
            this.Clock = null;
            this.PlayerCar = null;
            this.Ball = null;
            ap.Disengage();
        }

        public void LocateCar(Object sender, DoWorkEventArgs args)
        {
            this.PlayerCar = Car.GetCar(this.IgnoredCarAddresses, this.CarInfoPanel);

            while (this.PlayerCar == null)
            {
                this.PlayerCar = Car.GetCar(this.IgnoredCarAddresses, this.CarInfoPanel);
            }

            if (this.CarFound())
            {
              //  ap.Engage();
            }
        }

        public void LocateBall(Object sender, DoWorkEventArgs args)
        {
            this.Ball = Ball.GetBall(this.IgnoredBallAddresses, this.BallInfoPanel);

            while (this.Ball == null)
            {
                this.Ball = Ball.GetBall(this.IgnoredBallAddresses, this.BallInfoPanel);
            }

            if (this.GameReady)
            {
            }
        }

        public float AngleToBall
        {
            get
            {
                if (this.Ball == null)
                {
                    return 0;
                }
                var dx = this.Ball.Y - this.PlayerCar.Y ;
                var dy = this.Ball.X -  this.PlayerCar.X;

                var angle = Math.Atan2(dy, dx);
                var degrees = angle*(180.0f/Math.PI);
                return (float) degrees;
            }
        }

        public float AngleToLocation(Location location)
        {
            if (location == null)
            {
                return 0;
            }
            var dx = location.Y - this.PlayerCar.Y;
            var dy = location.X - this.PlayerCar.X;

            var angle = Math.Atan2(dy, dx);
            var degrees = angle * (180.0f / Math.PI);
            return (float)degrees;
        }

        public double DistanceToBall()
        {
            var distance = GetDistanceBetweenObjects(this.PlayerCar, this.Ball);

            return distance;

        }
        public static double GetDistanceBetweenObjects(GameObject firstObject, GameObject secondObject)
        {
            if (firstObject == null || secondObject == null)
            {
                return 0;
            }
            double a = firstObject.X - secondObject.X;
            double b = firstObject.Y - secondObject.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }

    }
}
