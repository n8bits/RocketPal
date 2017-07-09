using RocketPal.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketPal.Memory
{
    public class MemoryScanner
    {
        public int CurrentPosition = 0;

        

        public static MemoryChunk FindMemoryChunk(float value, float tolerance)
        {
            return new MemoryChunk(FindFloatInMemory(value, tolerance), 20);
        }

        public static IList<MemoryChunk> FindMemoryChunks(float value, float tolerance, int count)
        {
            List<MemoryChunk> chunks = new List<MemoryChunk>();
            int blockSize = 1048576 * 16;

            Process process = Process.GetProcessesByName("rocketleague")[0];
            IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);

            var currentBlock = (int)process.MainModule.BaseAddress;
            var totalBytes = process.PeakWorkingSet64 * 16;

            var totalBytesRead = 0;

            //while (totalBytesRead < totalBytes)
            while (chunks.Count() < count)
            {
                var bytesToRead = (totalBytes - totalBytesRead < blockSize) ? totalBytes - totalBytesRead : blockSize;
                int lpBytesRead = 0;

                var buffer = MemoryEdits.ReadMemory(currentBlock, bytesToRead, ref lpBytesRead);

                for (int i = 0; i < buffer.Length; i += 4)
                {
                    var thisFloat = BitConverter.ToSingle(buffer, i);

                    if (Math.Abs(thisFloat - value) < tolerance)
                    {
                        chunks.Add(new MemoryChunk(currentBlock + i, 10));
                    }
                }

                currentBlock += blockSize;
                totalBytesRead += (int)bytesToRead;

                if (totalBytesRead >= totalBytes)
                {
                    currentBlock = (int)process.MainModule.BaseAddress;
                    totalBytesRead = 0;
                }
            }

            return chunks;
        }

        public static List<int> FindByteArrayInMemory(byte[] bytes)
        {
            List<int> addresses = new List<int>();
            var regions = MemoryEdits.DoMemoryThing();

            var smallRegions = regions.ToList();
            smallRegions.Sort(delegate (KeyValuePair<int, int> x, KeyValuePair<int, int> y)
            {
                return x.Key > y.Key ? -1 : 1;
            });
            Process process = Process.GetProcessesByName("rocketleague")[0];
            IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);


            var totalBytes = 0;
            foreach (var keyValuePair in smallRegions)
            {
                totalBytes += keyValuePair.Value;
            }

            for (int j = 0; j < smallRegions.Count; j++)
            {

                if (j == smallRegions.Count - 1)
                {
                    var x = 2;
                }
                var region = smallRegions[j];
                var baseAddress = region.Key;
                var size = region.Value;

                if (baseAddress < 0x3FA61E6D && baseAddress + size> 0x3FA61E6D)
                {
                    var x = 3;
                }
                else
                {
                    continue;
                }

                var buffer = MemoryEdits.ReadMemory(hProcess1, region.Key, region.Value);

                for (int i = 0; i < buffer.Length; i += 4)
                {
                    var matches = PatternAt(buffer, new byte[] { 0,0,0});

                    if (matches.Any())
                    {
                        addresses.Add(matches.First());
                    }
                }
            }

            MemoryEdits.CloseHandle(hProcess1);

            return addresses;
        }

        public static List<int> FindByteSignatureInMemory(ByteMemorySignature signature)
        {
            List<int> addresses = new List<int>();
            var regions = MemoryEdits.DoMemoryThing();
            regions.Reverse();

            Process process = Process.GetProcessesByName("rocketleague")[0];
            IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);

            foreach (var region in regions)
            {

                var buffer = MemoryEdits.ReadMemory(hProcess1, region.Key, region.Value);
                var baseAddress = region.Key;
                var size = region.Value;

                if (baseAddress < 0x183D94B0 && baseAddress + size > 0x183D94B0)
                {
                    Console.Write("SADFSDf");
                }
                else
                {
                    //continue;
                }

                var matches = PatternAt(buffer, signature.bytes);

                if (matches.Any())
                {
                    addresses.Add(matches.First());
                }

            }

            if (addresses.Any() != true)
            {
                return FindByteSignatureInMemory(signature);
            }

            MemoryEdits.CloseHandle(hProcess1);
            return addresses;
        }

        public static IEnumerable<int> PatternAt(byte[] source, byte[] pattern)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                {
                    yield return i;
                }
            }
        }

        public static List<int> FindSignatureInMemory(MemorySignature signature, bool findAll = false, List<int> ignoredAddresses = null, MemoryScanInfoPanel panel = null)
        {
            List<int> addresses = new List<int>();
            var regions = MemoryEdits.DoMemoryThing();
            
            var smallRegions = regions.Where(x => x.Value == 65536).ToList();
            smallRegions.Reverse();
            smallRegions.Sort(delegate (KeyValuePair<int,int> x, KeyValuePair<int, int> y)
            {
                return x.Key > y.Key ? -1 : 1;
            });

            Process process = Process.GetProcessesByName("rocketleague")[0];
            IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);
            

            var totalBytes = 0;
            foreach (var keyValuePair in smallRegions)
            {
                totalBytes += keyValuePair.Value;
            }

            for (int j = 0; j < smallRegions.Count; j++)
            {
                if (panel != null)
                {
                    panel.SetCurrentBlock(j, smallRegions.Count());
                }

                if (j == smallRegions.Count - 1)
                {
                    var x = 2;
                }
                var region = smallRegions[j];
                var baseAddress = region.Key;
                var size = region.Value;

                if (baseAddress < 0x3FA61E6D && baseAddress + size > 0x3FA61E6D)
                {
                    //var index = regions.ToList().IndexOf(region);
                    //var z = regions.Where(x => x.Value != size).Select(x => x.Value).ToList();
                    //string s = "";
                    //var buffer1 = MemoryEdits.ReadMemory(region.Key, region.Value);
                    //foreach (var b in buffer1)
                    //{
                    //    s += b;
                    //    if (b < 10)
                    //    {
                    //        s += " ";
                    //    }
                    //    if (b < 100)
                    //    {
                    //        s += " ";
                    //    }
                    //    s += " ";
                    var x = 3;
                    //}
                    //Clipboard.SetText(s);
                }
                else
                {
                 // continue;
                }

                var buffer = MemoryEdits.ReadMemory(hProcess1, region.Key, region.Value);
                //continue;
                for (int i = 0; i < buffer.Length; i += 4)
                {
                    var thisFloat = BitConverter.ToSingle(buffer, i);

                    if (signature.SearchKey.Contains(thisFloat))
                    {
                        if (signature.MatchAtKeyAddress(baseAddress + i))
                        {
                            if (ignoredAddresses == null || !ignoredAddresses.Contains(baseAddress + i))
                            {
                                addresses.Add((baseAddress + i) - (signature.SearchKeyIndex * 4));
                            }
                        }
                    }
                    if (addresses.Count() >= 1 || (addresses.Count >=1&& signature.Length < 20))
                    {
                        if (!findAll)
                        {
                            return addresses;
                        }
                    }
                }
            }

            MemoryEdits.CloseHandle(hProcess1);
            
            return addresses;
        }

        private void SearcherThread(object sender, DoWorkEventArgs args)
        {
            
        }

        public static int FindFloatInMemory(float value, float tolerance)
        {
            int blockSize = 1048576 * 16;

            Process process = Process.GetProcessesByName("rocketleague")[0];
            IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);

            var currentBlock = (int)process.MainModule.BaseAddress;
            var totalBytes = process.PeakWorkingSet64 * 16;

            var totalBytesRead = 0;

            //while (totalBytesRead < totalBytes)
            while (true)
            {
                var bytesToRead = (totalBytes - totalBytesRead < blockSize) ? totalBytes - totalBytesRead : blockSize;
                int lpBytesRead = 0;

                var buffer = MemoryEdits.ReadMemory(currentBlock, bytesToRead, ref lpBytesRead);

                for (int i = 0; i < buffer.Length; i += 4)
                {
                    var thisFloat = BitConverter.ToSingle(buffer, i);

                    if (Math.Abs(thisFloat - value) < tolerance)
                    {
                        return currentBlock + i;
                    }
                }

                currentBlock += blockSize;
                totalBytesRead += (int)bytesToRead;

                if (totalBytesRead >= totalBytes)
                {
                    currentBlock = (int)process.MainModule.BaseAddress;
                    totalBytesRead = 0;
                }
            }

            return -1;
        }
    }
}
