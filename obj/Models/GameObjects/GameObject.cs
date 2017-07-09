using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Memory;

namespace RocketPal.Models.GameObjects
{

    public class GameObject
    {

        public static readonly float arenaSizeX = 5000f;

        public static readonly float arenaSizeY = 6000f;

        public static readonly float WallBoundryX = 4000;

        public static readonly float WallBoundryY = 5000;

        public List<int> Addresseses;
        public int MemoryAddress { get; set; }

        public GameObject(List<int> addresses, int xPositionMemoryOffset, int yPositionMemoryOffset, int zPositionMemoryOffset)
        {
            
            this.Addresseses = addresses;
            this.MemoryAddress = addresses[0];
            this.XPositionMemoryOffset = xPositionMemoryOffset;
            this.YPositionMemoryOffset = yPositionMemoryOffset;
            this.ZPositionMemoryOffset = zPositionMemoryOffset;
        }

        //protected static int GetObjectAddress(float[] objectSignature)
        //{
        //    var occurence = 3;
        //    var address = -1;
        //    var objectFound = false;

        //    while (!objectFound)
        //    {
        //        address = MemoryEdits.FindFloatSequenceInMemory(objectSignature, .001f);
        //        var x = MemoryEdits.ReadFloat(address + XPositionMemoryOffset);
        //        var y = MemoryEdits.ReadFloat(address + YPositionMemoryOffset);

        //        if (x > -6000f && 
        //            x < 6000f && 
        //            y > -10000f && 
        //            y < 10000f &&
        //            Math.Abs(x) > 1f &&
        //            Math.Abs(y) > 1f
        //            )
        //        {
        //            objectFound = true;
        //        }
        //        else
        //        {
        //            occurence++;
        //        }
        //    }


        //    return address;
        //}

        protected static List<int> LocateObject(MemorySignature signature, bool findAll = false, List<int> ignoredAddress = null)
        {
            return MemoryScanner.FindSignatureInMemory(signature, findAll, ignoredAddress);
        }

        public Location Location => new Location(this.X, this.Y, this.Z, 0f);

        public static bool CheckObjectsWithinBounds(GameObject[] objects)
        {
            foreach (var o in objects)
            {
                if (o.X < -arenaSizeX || o.X > arenaSizeX || o.Y < -arenaSizeY || o.Y > arenaSizeY)
                {
                    return false;
                }
            }

            return true;
        }

        protected int XPositionMemoryOffset { get; }

        protected int YPositionMemoryOffset { get; }

        protected int ZPositionMemoryOffset { get; }

        public float X => MemoryEdits.ReadFloat((this.MemoryAddress + XPositionMemoryOffset));

        public float Y => MemoryEdits.ReadFloat((this.MemoryAddress + YPositionMemoryOffset));

        public float Z => MemoryEdits.ReadFloat((this.MemoryAddress + ZPositionMemoryOffset));

        public void UseNextAddress()
        {
            var currentAddressIndex = this.Addresseses.IndexOf(this.MemoryAddress);
            currentAddressIndex++;

            if (currentAddressIndex >= this.Addresseses.Count())
            {
                currentAddressIndex = 0;
            }

            this.MemoryAddress = Addresseses[currentAddressIndex];
        }

        public void UsePreviousAddress()
        {
            var currentAddressIndex = this.Addresseses.IndexOf(this.MemoryAddress);

            if (currentAddressIndex == 0)
            {
                currentAddressIndex = this.Addresseses.Count() - 1;
            }

            this.MemoryAddress = Addresseses[currentAddressIndex];
        }
    }
}
