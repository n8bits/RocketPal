using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Models.GameObjects
{
    public class Location
    {
        public float X, Y, Z;
        public double Radius;

        public Location(float x, float y, float z, double radius = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Radius = radius;
        }

        public double DistanceTo(Location location)
        {
            return Distance(this, location);
        }

        public static double Distance(Location first, Location second)
        {
            if (first == null || second == null)
            {
                return 0;
            }
            double a = first.X - second.X;
            double b = first.Y - second.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }
    }
}
