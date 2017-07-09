using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Components;
using RocketPal.Memory;
using RocketPal.Models.GameObjects;

namespace RocketPal.Models.GameElements
{
    public class GameClock : GameObject
    {
        public static readonly int clockOffset = -(13 * 4);

        //Works for exhibition
        //public static readonly byte[] locatorBytes = new byte[]
        //    {0x2C, 0x01, 0x00, 0x00,
        //     0x03, 0x00, 0x00, 0x00,
        //     0x00, 0x00, 0x00, 0x00};

        public static readonly byte[] locatorBytes = new byte[]
        {
            0x01, 0x00, 0x00, 0x00,
            0x04, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x40, 0x40,
            0x00, 0x00, 0x00, 0x00
        };

        public static readonly MemorySignature signature = new MemorySignature(
           new List<FloatRange>()
           {
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 0), BitConverter.ToSingle(locatorBytes, 0)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 4), BitConverter.ToSingle(locatorBytes, 4)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 8), BitConverter.ToSingle(locatorBytes, 8)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 12), BitConverter.ToSingle(locatorBytes, 12)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 16), BitConverter.ToSingle(locatorBytes, 16)}),
           }, 3
       );

        // public static readonly MemorySignature signature = new MemorySignature(
        //    new List<FloatRange>()
        //    {
        //         new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 0), BitConverter.ToSingle(locatorBytes, 0)}),
        //         new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 4), BitConverter.ToSingle(locatorBytes, 4)}),
        //         new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 8), BitConverter.ToSingle(locatorBytes, 8)}),
        //         FloatRange.Any(),
        //         FloatRange.Any(),
        //         FloatRange.Any(),
        //         FloatRange.Any(),
        //         FloatRange.Any(),
        //         new FloatRange(2, 300)

        //    }, 0
        //);

        public GameClock(List<int> addresses ) : base(addresses, 0,0,0)
        {
        }

        public static GameClock GetClock(MemoryScanInfoPanel infoPanel = null)
        {
            var foundAddresses = MemoryScanner.FindSignatureInMemory(GameClock.signature, false, null, infoPanel);
            if (foundAddresses.Any())
            {
                return new GameClock(foundAddresses);
            }
            else return null;
        }

        public Int32 TimeRemaining
        {
            get
            {
                var bytes = MemoryEdits.ReadMemory(this.MemoryAddress + clockOffset, 4);
                return BitConverter.ToInt32(bytes, 0);
            }
        } 
    }
}
