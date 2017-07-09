using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketPal.Memory;

namespace RocketPal.Models
{
    public abstract class GameElement
    {
        public ByteMemorySignature Signature;

        public readonly int memoryLocation;

        public GameElement(byte[] bytes)
        {
            this.memoryLocation = MemoryScanner.FindSignatureInMemory(new MemorySignature(bytes)).First();
        }
    }
}
