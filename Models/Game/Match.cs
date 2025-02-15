﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RocketPal.Models.GameElements;

namespace RocketPal.Models.Game
{
    public class Match
    {
        public readonly GameClock GameClock = null;

        public Match(GameClock clock)
        {
            this.MatchComplete = false;
            this.GameClock = clock;
            this.WatchClock();
        }

        private void WatchClock()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += this.ClockWatcher;
            worker.RunWorkerAsync();
        }

        private void ClockWatcher(Object sender, DoWorkEventArgs args)
        {
            while (this.GameClock.TimeRemaining > 0)
            {
                Console.WriteLine("Clock:  " + this.GameClock.TimeRemaining);
                if (this.GameClock.TimeRemaining > 10000)
                {
                    Console.WriteLine("Bad clock data, forcing round end...");
                    break;
                }
                Thread.Sleep(400);
            }

            var overtime = false;

            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (watch.ElapsedMilliseconds < 20)
            {
                if (this.GameClock.TimeRemaining > 1)
                {
                    Console.WriteLine("Overtime detected....");

                    var remaining = this.GameClock.TimeRemaining - watch.Elapsed.Seconds;

                    while (this.GameClock.TimeRemaining - watch.Elapsed.Seconds > remaining + 1)
                    {
                        remaining++;
                        Thread.Sleep(1000);
                        Console.Write("+" + this.GameClock.TimeRemaining);
                    }
                }
            }

            this.MatchComplete = true;
            Console.WriteLine("Match is complete in clockwatcher.");
        }

        public bool MatchComplete { get; private set; }
    }
}
