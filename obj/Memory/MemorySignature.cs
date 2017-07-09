using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Memory;

namespace RocketPal.Memory
{

    public class MemorySignature
    {
        public List<FloatRange> floatRanges { get; set; }

        public int SearchKeyIndex { get; }

        public MemorySignature(List<FloatRange> ranges, int searchKeyIndex)
        {
            this.floatRanges = ranges;
            this.SearchKeyIndex = searchKeyIndex;
        }

        public MemorySignature(byte[] byteArray)
        {
            List<byte> bytes = byteArray.ToList();
            while (bytes.Count()%4 != 0)
            {
                bytes.Add(0);
            }
            List<FloatRange> floatRanges = new List<FloatRange>();
            var asArray = bytes.ToArray();
            for (int i = 0; i < bytes.Count(); i += 4)
            {
                var floatConversion = BitConverter.ToSingle(asArray, i);
                var range = new FloatRange(new float[] { floatConversion, floatConversion });
                floatRanges.Add(range);
            }

            this.floatRanges = floatRanges;
        }

        public FloatRange SearchKey => this.floatRanges[this.SearchKeyIndex];

        public FloatRange First
        {
            get { return this.floatRanges.First(); }
        }

        public bool CompareToChunk(MemoryChunk chunk)
        {
            for (int i = 0; i < this.Length; i++)
            {
                if (!this.floatRanges[i].Contains(chunk.Values[i]))
                {
                    return false;
                }
                if (i > 6)
                {
                    var x = 6;
                }
            }

            return true;
        }

        public bool MatchAtKeyAddress(int keyAddress)
        {
            var chunk = new MemoryChunk(keyAddress, (this.SearchKeyIndex), this.floatRanges.Count());

            for (int i = 0; i < this.floatRanges.Count(); i++)
            {
                if (!this.floatRanges[i].Contains(chunk.Values[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int Length => this.floatRanges.Count();
    }
}
