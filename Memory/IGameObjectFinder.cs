using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Memory
{
    interface IGameObjectFinder
    {
        IDictionary<BackgroundWorker, MemorySignature> SearchRequest { get;}

    }
}
