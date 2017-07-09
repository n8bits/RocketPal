using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Components;
using RocketPal.Memory;

namespace RocketPal.Models.GameObjects
{
    public class Ball : GameObject
    {
        public static readonly float BallHeight = 93.140f;

        public static readonly float[] ballSignature = new float[] { 93.140f };

        public static readonly byte[] locatorBytes = {0xCD, 0x6C, 0xD8, 0x44};

        public static readonly MemorySignature signature = new MemorySignature(
           new List<FloatRange>()
           {

               new FloatRange(BitConverter.ToSingle(locatorBytes, 0), BitConverter.ToSingle(locatorBytes, 0)),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               FloatRange.Any(),
               new FloatRange(new float[] {-arenaSizeX, -1f, 1f, arenaSizeX}),
               new FloatRange(new float[] {-arenaSizeY, -1f, 1f, arenaSizeY}),
               new FloatRange(92.14f, 3093.15f),
               new FloatRange(1f, 1f),
               FloatRange.NaN()
           }, 0
       );

        private Ball(List<int> addresses) : base(addresses, (4 * 18), (4 * 19), (4 * 20))
        {

        }

        public static Ball GetBall(List<int> ignoredAddresses = null, MemoryScanInfoPanel infoPanel = null)
        {
            var locations = MemoryScanner.FindSignatureInMemory(Ball.signature, false, ignoredAddresses, infoPanel);

            if (!locations.Any())
            {
                return null;
            }
            var ball = new Ball(locations);
            //ball.UseNextAddress();
            return ball;
        }
        
    }
}
