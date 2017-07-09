using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Models.GameObjects
{
    public class Peanut
    {
        public static readonly double PeanutRadius = 300d;

        public static readonly Location CornerPeanutLocation0 = new Location(-3000f, -4000f, 0, PeanutRadius);

        public static readonly Location CornerPeanutLocation1 = new Location(3000f, -4000f, 0, PeanutRadius);

        public static readonly Location CornerPeanutLocation2 = new Location(-3000f, 4000f, 0, PeanutRadius);

        public static readonly Location CornerPeanutLocation3 = new Location(3000f, 4000f, 0, PeanutRadius);

        public static readonly Location MidfieldPeanutLocation0 = new Location(3000f, 0f, 0, PeanutRadius);

        public static readonly Location MidfieldPeanutLocation1 = new Location(-3000f, 0f, 0, PeanutRadius);

        public static readonly Location[] PeanutLocations =
        {
            CornerPeanutLocation0, CornerPeanutLocation1,
            CornerPeanutLocation2, CornerPeanutLocation3,
            MidfieldPeanutLocation0, MidfieldPeanutLocation1
        };

        public readonly Location Location;

        public Peanut(Location location)
        {
            this.Location = location;
        }

        public static Location NearestPeanut(Location startLocation)
        {
            Dictionary<Location, double> distances = new Dictionary<Location, double>();

            foreach (var peanutLocation in PeanutLocations)
            {
                distances.Add(peanutLocation, GameObjects.Location.Distance(startLocation, peanutLocation));
            }
            var nearestPeanut = distances.First();

            foreach (var distance in distances)
            {
                if (distance.Value < nearestPeanut.Value)
                {
                    nearestPeanut = distance;
                }
            }

            return nearestPeanut.Key;
        }
    }
}
