using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RocketPal.Memory;
using RocketPal.Components;

namespace RocketPal.Models.GameObjects
{
    public class Car : GameObject
    {
        public static readonly float[] carSignature = new float[] { 18.356f, 1.000f };

        public static readonly float breakoutHeight = 13.2625f;

        public static readonly float dominusHeight = 18.36f;

        public static readonly float octaneHeight = 17.0207f;

        public static readonly int MemoryOffset_X = -(21*4);

        public static readonly int MemoryOffset_Y = -(20 * 4);

        public static readonly int MemoryOffset_Z = -(20 * 4);

        public static readonly int MemoryOffset_XRot = (4 * 4);

        public static readonly int MemoryOffset_YRot = (5 * 4);

        public static readonly byte[] locatorBytes =
        {
            0x00, 0x00, 0x80, 0x3F,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00,
            0x0F, 0xB4, 0x00, 0x00,
            0x04, 0x11, 0x6A, 0x32,
            0x81, 0x02, 0x18, 0x06
            //0x00, 0xCB, 0x67, 0x0E,
            //0x00, 0x00, 0x00, 0x00,
            //0x00, 0x00, 0x00, 0x00,
            //0x00, 0x00, 0x00, 0x00,
            //0x00, 0x00, 0x00, 0x00,
            //0x00, 0x00, 0x00, 0x00, 
        };

        

        public static readonly MemorySignature signature = new MemorySignature(
            new List<FloatRange>()
            {
                new FloatRange(-1, 1),  //0.67
                new FloatRange(-1, 1), // 0.75
                new FloatRange(-1, 1), // -.02
                new FloatRange(-1, 1), // 0
                new FloatRange(-1, 1), //-.75
                new FloatRange(-1, 1), // -.67
                new FloatRange(-1, 1), // not quiet 0 
                new FloatRange(0, 0), // 0
                new FloatRange(-1, 1), //-.01 no idea
                new FloatRange(-1, 1), // 0.01 no idea
                new FloatRange(-1, 1), // ToggleVisible to 1?
                new FloatRange(0, 0), // Unknown
                new FloatRange(new float[] {-arenaSizeX, -3140f, -3130f, -1f, 1f, arenaSizeX}),
                new FloatRange(new float[] {-arenaSizeY, -3140f, -3130f, -1f, 1f, arenaSizeY}),
                new FloatRange(octaneHeight - .01f, octaneHeight + .01f),

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
                FloatRange.Any(),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 0), BitConverter.ToSingle(locatorBytes, 0)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 4), BitConverter.ToSingle(locatorBytes, 4)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 8), BitConverter.ToSingle(locatorBytes, 8)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 12), BitConverter.ToSingle(locatorBytes, 12)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 16), BitConverter.ToSingle(locatorBytes, 16)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 20), BitConverter.ToSingle(locatorBytes, 20)}),
                new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 24), BitConverter.ToSingle(locatorBytes, 24)}),
                //**************************************
                //new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 0), BitConverter.ToSingle(locatorBytes, 0)}),
                //new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 4), BitConverter.ToSingle(locatorBytes, 4)}),
                //new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 8), BitConverter.ToSingle(locatorBytes, 8)}),
                //new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 12), BitConverter.ToSingle(locatorBytes, 12)}),
                //new FloatRange(new float[] {BitConverter.ToSingle(locatorBytes, 16), BitConverter.ToSingle(locatorBytes, 16)}),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //new FloatRange(-1, 1),  //0.67
                //new FloatRange(-1, 1), // 0.75
                //new FloatRange(-1, 1), // -.02
                //new FloatRange(-1, 1), // 0
                //new FloatRange(-1, 1), //-.75
                //new FloatRange(-1, 1), // -.67
                //new FloatRange(-1, 1), // not quiet 0 
                //new FloatRange(0, 0), // 0
                //new FloatRange(-1, 1), //-.01 no idea
                //new FloatRange(-1, 1), // 0.01 no idea
                //new FloatRange(-1, 1), // ToggleVisible to 1?
                //new FloatRange(0, 0), // Unknown
                //new FloatRange(new float[] {-arenaSizeX, -3140f, -3130f, -1f, 1f, arenaSizeX}),
                //new FloatRange(new float[] {-arenaSizeY, -3140f, -3130f, -1f, 1f, arenaSizeY}),
                //new FloatRange(breakoutHeight - .001f, breakoutHeight + .001f),
                //new FloatRange(1.0f, 1.0f),
                //FloatRange.Any(),
                //new FloatRange(0.0f, 0.0f)


                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //FloatRange.Any(),
                //new FloatRange(-1.0f, 1.0f),
                //new FloatRange(-1.0f, 1.0f),
                //new FloatRange(-1.0f, 1.0f),
            }, 14
        );
        

        private Car(List<int> addresses) : base(addresses, (4 * 12), (4 * 13), (4 * 14))
        {
        }

        public float RotationSin
        {
            get
            {
                var sin = MemoryEdits.ReadFloat(this.MemoryAddress + MemoryOffset_XRot);
                var cos = MemoryEdits.ReadFloat(this.MemoryAddress + MemoryOffset_YRot);

                return sin;
            }
        }

        public float RotationCos
        {
            get
            {
                var sin = MemoryEdits.ReadFloat(this.MemoryAddress + MemoryOffset_XRot);//this.MemoryAddress + (26 * 4));
                var cos = MemoryEdits.ReadFloat(this.MemoryAddress + MemoryOffset_YRot);
                
                return cos;
            }
        }

        public double RotationZ
        {
            get
            {
               

                var angle = Math.Atan2(this.RotationSin, this.RotationCos);
                var degrees = (angle * (180f / Math.PI));

                degrees -= 90;
                if (degrees < -180)
                {
                    degrees = -(-360 - degrees);
                }
                else if (degrees > 180)
                {
                    degrees = -(360 - degrees);
                }

                return degrees;
            }
        }

        public static Car GetCar(List<int> ignoredAddresses = null, MemoryScanInfoPanel infoPanel = null)
        {
            var results = MemoryScanner.FindSignatureInMemory(Car.signature, false,  ignoredAddresses, infoPanel);

            //foreach (var result in results)
            //{
            //    var bytes = MemoryEdits.ReadMemory(result, (6*4));
            //    var a1 = Math.Abs(MemoryEdits.ReadFloat(result));
            //    var b1 = Math.Abs(MemoryEdits.ReadFloat(result + 4));
            //    var a2 = Math.Abs(MemoryEdits.ReadFloat(result + 20));
            //    var b2 = Math.Abs(MemoryEdits.ReadFloat(result + 16));
            //    if (Math.Abs(a1 - a2) > .001f && Math.Abs(b1 - b2) > .1f)
            //    {
            //        results = results.Where(x => x != result).ToList();
            //    }
            //}
            if (results.Any() != true)
            {
                return null;
            }
            return new Car(results);
        }
        
    }
}
