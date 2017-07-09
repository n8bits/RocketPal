using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Memory
{
    public class MemoryChunk
    {
        public int BaseAddress { get; }

        public float[] Values;

        private int width;

        public MemoryChunk(int baseAddress, int width)
        {
            this.BaseAddress = baseAddress;
            this.width = width;

            Values = new float[(width * 2) + 1];

            var bytes = MemoryEdits.ReadMemory(BaseAddress - (width * 4), (width * 4 * 2) + 4);

            for (int i = 0; i < bytes.Length; i += 4)
            {
                Values[i / 4] = BitConverter.ToSingle(bytes, i);
            }
        }

        public MemoryChunk(int keyAddress, int negativeExtent, int postitiveExtend)
        {
            this.BaseAddress = keyAddress;

            int totalSize = (negativeExtent + postitiveExtend + 1) * 4;

            this.Values = new float[totalSize / 4];

            int start = keyAddress - (negativeExtent * 4);

            var bytes = MemoryEdits.ReadMemory(start, totalSize);

            for (int i = 0; i < bytes.Length; i += 4)
            {
                Values[i / 4] = BitConverter.ToSingle(bytes, i);
            }
        }

        public MemoryChunk(int baseAddress, int chunkSize, bool readForward)
        {
            this.BaseAddress = baseAddress;
            this.width = chunkSize / 2;

            Values = new float[chunkSize];

            var bytes = MemoryEdits.ReadMemory(BaseAddress, (chunkSize * 4));

            for (int i = 0; i < bytes.Length; i += 4)
            {
                Values[i / 4] = BitConverter.ToSingle(bytes, i);
            }
        }

        public float GetValue(int offset)
        {
            return this.Values[offset + this.width];
        }

    }
}
