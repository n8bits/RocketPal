using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Memory;
using RocketPal.Models.GameObjects;

namespace RocketPal.Models.GameElements
{

    public class BoostMeter : GameObject
    {
        public static readonly byte[] locatorBytes = new byte[]
        {
            0x00, 0x00, 0x00, 0x02,
            0x00, 0x00, 0x00, 0x07,
            0x00, 0x00, 0x00, 0xFE,
            0xFF, 0xFF, 0xFF, 0x40
        };

        public static readonly MemorySignature signature = new MemorySignature(
           new List<FloatRange>()
           {
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 0), BitConverter.ToSingle(locatorBytes, 0)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 4), BitConverter.ToSingle(locatorBytes, 4)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 8), BitConverter.ToSingle(locatorBytes, 8)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 12), BitConverter.ToSingle(locatorBytes, 12)}),
           }, 0
       );

        public BoostMeter(List<int> addresses) : base(addresses, 0,0,0)
        {
        }

        public static BoostMeter GetBoostMeter()
        {
            var foundAddresses = MemoryScanner.FindByteArrayInMemory(BoostMeter.locatorBytes);
            if (foundAddresses.Any())
            {
                return new BoostMeter(foundAddresses);
            }
            else return null;
        }

        public int RemainingBoost
        {
            get
            {
                return MemoryEdits.ReadMemory(this.MemoryAddress - 1, 1)[0];
            }
        }
    }
}
