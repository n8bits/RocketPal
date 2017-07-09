using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Extensions
{
    public static class StopwatchExtensions
    {

        public static long ElapsedNanoseconds(this Stopwatch watch)
        {
            var nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;

            return watch.ElapsedTicks * nanosecPerTick;
        }

        public static long NanoSecondsToTicks(long nanoseconds)
        {
            var nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;

            return nanoseconds*nanosecPerTick;
        }
    }
}
