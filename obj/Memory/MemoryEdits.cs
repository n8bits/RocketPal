// Decompiled with JetBrains decompiler
// Type: RocketLeagueTrainer.MemoryEdits
// Assembly: RocketLeagueTrainer, Version=0.4.1.0, Culture=neutral, PublicKeyToken=null
// MVID: B405902E-449B-4D89-9ED1-934142701DD9
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\rocketleague\RocketLeagueTrainer_v0_4_1\RocketLeagueTrainer.exe

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using RocketPal.Memory;

namespace RocketPal.Memory
{
    internal static class MemoryEdits
    {
        public static Process process;
        public static IntPtr startAddress;
        
        private const int PROCESS_VM_WRITE = 32;
        private const int PROCESS_VM_OPERATION = 8;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(IntPtr hProcess);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress,
        out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;  // minimum address
            public IntPtr maximumApplicationAddress;  // maximum address
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }

        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;   // size of the region allocated by the program
            public int State;   // check if allocated (MEM_COMMIT)
            public int Protect; // page protection (must be PAGE_READWRITE)
            public int lType;
        }

        public static Dictionary<int, int>  DoMemoryThing()
        {
            // getting minimum & maximum address
            Dictionary<int,int> regions = new Dictionary<int, int>();

            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(out sys_info);

            IntPtr proc_min_address = sys_info.minimumApplicationAddress;
            IntPtr proc_max_address = sys_info.maximumApplicationAddress;

            // saving the values as long ints so I won't have to do a lot of casts later
            long proc_min_address_l = (long)proc_min_address;
            long proc_max_address_l = (long)proc_max_address;


            // notepad better be runnin'
            Process process = Process.GetProcessesByName("rocketleague")[0];

            // opening the process with desired access level
            IntPtr processHandle =
            OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, process.Id);

            //StreamWriter sw = new StreamWriter("dump.txt");

            // this will store any information we get from VirtualQueryEx()
            MEMORY_BASIC_INFORMATION mem_basic_info = new MEMORY_BASIC_INFORMATION();

            int bytesRead = 0;  // number of bytes read with ReadProcessMemory

            while (proc_min_address_l < proc_max_address_l)
            {
                // 28 = sizeof(MEMORY_BASIC_INFORMATION)
                VirtualQueryEx(processHandle, proc_min_address, out mem_basic_info, 28);

                // if this memory chunk is accessible
                if (mem_basic_info.Protect ==
                PAGE_READWRITE && mem_basic_info.State == MEM_COMMIT)
                {
                    //byte[] buffer = new byte[mem_basic_info.RegionSize];

                    // read everything in the buffer above
                    //ReadProcessMemory((int)processHandle,
                    //mem_basic_info.BaseAddress, buffer, mem_basic_info.RegionSize, ref bytesRead);

                    //// then output this in the file
                    //var bytes = ReadMemory(mem_basic_info.BaseAddress, mem_basic_info.RegionSize);
                    //for (int i = 0; i < bytes.Length; i += 4)
                    //{
                    //    var f = BitConverter.ToSingle(bytes, i);
                    //    if (f > 18.3562f && f < 18.3563f)
                    //    {
                    //        var z = 3;
                    //    }
                    //}
                    // sw.WriteLine("0x{0} : {1}", (mem_basic_info.BaseAddress + i).ToString("X"), (char)buffer[i]);
                    
                    if (mem_basic_info.lType == 0x20000)
                    {
                        regions.Add(mem_basic_info.BaseAddress, mem_basic_info.RegionSize);
                    }

                }

                // move to the next memory chunk
                proc_min_address_l += mem_basic_info.RegionSize;
                proc_min_address = new IntPtr(proc_min_address_l);
            }

            //sw.Close();

            return regions;
        }
    

    //[DllImport("kernel32.dll", SetLastError = true)]
    //private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

    public static bool hook(Process p)
        {
            try
            {
                MemoryEdits.process = p;
                MemoryEdits.startAddress = p.MainModule.BaseAddress;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Error accessing process: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            return true;
        }

        //public static IntPtr allocMemory()
        //{
        //    IntPtr hProcess = MemoryEdits.OpenProcess(2035711, false, MemoryEdits.process.Id);
        //    UIntPtr num1 = new UIntPtr(1024U);
        //    IntPtr lpAddress = new IntPtr(0);
        //    IntPtr num2 = (IntPtr)num1;
        //    int num3 = 4096;
        //    int num4 = 64;
        //    return MemoryEdits.VirtualAllocEx(hProcess, lpAddress, (UIntPtr)num2, (AllocationType)num3, (MemoryProtection)num4);
        //}

        //public static void writeMem(int address, byte[] buffer)
        //{
        //    IntPtr hProcess1 = MemoryEdits.OpenProcess(2035711, false, MemoryEdits.process.Id);
        //    int num = 0;
        //    int hProcess2 = (int)hProcess1;
        //    int lpBaseAddress = address;
        //    byte[] lpBuffer = buffer;
        //    int Length = lpBuffer.Length;
        //    // ISSUE: explicit reference operation
        //    // ISSUE: variable of a reference type
        //    int&lpNumberOfBytesWritten = @num;
        //    MemoryEdits.WriteProcessMemory(hProcess2, lpBaseAddress, lpBuffer, Length, lpNumberOfBytesWritten);
        //    MemoryEdits.CloseHandle(hProcess1);
        //}

        public static byte[] OffsetReadMem(int address, int bytesToRead)
        {
            Process process = Process.GetProcessesByName("rocketleague")[0];
            IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);

            byte[] numArray = new byte[bytesToRead];
            int hProcess2 = (int)hProcess1;
            int lpBaseAddress = address;
            byte[] lpBuffer = numArray;
            int length = lpBuffer.Length;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            int lpNumberOfBytesRead = 0;
            MemoryEdits.ReadProcessMemory(hProcess2, (int)process.MainModule.BaseAddress + lpBaseAddress, lpBuffer, length, ref lpNumberOfBytesRead);
            MemoryEdits.CloseHandle(hProcess1);
            return numArray;
        }

        public static int FindFloatInMemory(float value, int offset, float tolerance)
        {
            int blockSize = 1048576;

            Process process = Process.GetProcessesByName("rocketleague")[0];
            IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);

            var currentBlock = (int)process.MainModule.BaseAddress + offset;
            var totalBytes = process.PeakWorkingSet64;

            var totalBytesRead = 0;

            //while (totalBytesRead < totalBytes)
            while (true)
            {
                var bytesToRead = (totalBytes - totalBytesRead < blockSize) ? totalBytes - totalBytesRead : blockSize;
                int lpBytesRead = 0;

                var buffer = ReadMemory(currentBlock, bytesToRead, ref lpBytesRead);

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

                if (totalBytesRead >= process.PeakWorkingSet64 * 16)
                {
                    currentBlock = (int)process.PeakWorkingSet64 * 1024;
                    totalBytesRead = 0;
                }
            }

            return -1;
        }

        public static int FindFloatSequenceInMemory(float[] floats, float tolerance)
        {
            IList<MemoryChunk> found = null;
            var searchIndex = 0;

            var matchFound = false;

            while (matchFound == false)
            {
                found = MemoryScanner.FindMemoryChunks(floats[0], tolerance, 4);

                foreach (var memoryChunk in found)
                {
                    var oneFoward = memoryChunk.GetValue(1);

                    if (oneFoward - 1.0f < tolerance)
                    {
                        matchFound = true;
                        return memoryChunk.BaseAddress;
                    }
                }
            }
            return found[3].BaseAddress;
        }

        public static bool AreBytes(float[] array1, float[] array2, float tolerance)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                var difference = Math.Abs(array1[i] - array2[i]);

                if (difference > tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        public static byte[] ReadMemory(int address, long bytesToRead, ref int numberOfBytesRead)
        {
            Process process = Process.GetProcessesByName("rocketleague")[0];

            IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);

            byte[] numArray = new byte[bytesToRead];
            int hProcess2 = (int)hProcess1;
            int lpBaseAddress = address;
            byte[] lpBuffer = numArray;
            int length = lpBuffer.Length;

            MemoryEdits.ReadProcessMemory(hProcess2, address, lpBuffer, length, ref numberOfBytesRead);
            MemoryEdits.CloseHandle(hProcess1);

            //var remainingBytes = bytesToRead - lpNumberOfBytesRead;
            //if (remainingBytes > 0)
            //{
            //  numArray Array  ReadMemory(lpNumberOfBytesRead, remainingBytes);
            //}

            return lpBuffer;
        }

        public static byte[] ReadMemory(int address, long bytesToRead)
        {
            Process process = Process.GetProcessesByName("rocketleague")[0];
            IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);

            byte[] numArray = new byte[bytesToRead];
            int hProcess2 = (int)hProcess1;
            int lpBaseAddress = address;
            byte[] lpBuffer = numArray;
            int length = lpBuffer.Length;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            int lpNumberOfBytesRead = 0;
            MemoryEdits.ReadProcessMemory(hProcess2, address, lpBuffer, length, ref lpNumberOfBytesRead);
            MemoryEdits.CloseHandle(hProcess1);

            //var remainingBytes = bytesToRead - lpNumberOfBytesRead;
            //if (remainingBytes > 0)
            //{
            //  numArray Array  ReadMemory(lpNumberOfBytesRead, remainingBytes);
            //}

            return lpBuffer;
        }

        public static byte[] ReadMemory(IntPtr processHandle, int address, long bytesToRead)
        {
            byte[] numArray = new byte[bytesToRead];
            int hProcess2 = (int)processHandle;
            int lpBaseAddress = address;
            byte[] lpBuffer = numArray;
            int length = lpBuffer.Length;

            int lpNumberOfBytesRead = 0;
            MemoryEdits.ReadProcessMemory(hProcess2, address, lpBuffer, length, ref lpNumberOfBytesRead);
            
            return lpBuffer;
        }

        public static float ReadFloat(int address)
        {
            return BitConverter.ToSingle(MemoryEdits.ReadMemory(address, 4), 0);
        }

        public static float[] ReadFloats(Process process)
        {
            long memorySize = process.WorkingSet64;
            int chunkSize = (int)Math.Pow(2, 10);

            int baseAddress = (int)process.MainModule.BaseAddress;
            int memoryPointer = baseAddress;

            float[] floats = new float[memorySize / 4];

            var data = ReadMemory(0x32264668 - 10000, memorySize);
            //var data = ReadMemory(0, 0x33264668);
            for (Int32 i = 0; i < memorySize - 4; i += 1)
            {

                var thisFloat = BitConverter.ToSingle(data, i);
                if (Math.Abs(thisFloat - 871.27F) < .1f)
                {
                    Console.WriteLine(thisFloat + "\n");
                }

            }

            //for (int h = 0; h < memorySize / chunkSize; h++)
            //{

            //    byte[] lpBuffer = new byte[chunkSize]; ;
            //    var bytes = ReadMemory(memoryPointer, chunkSize);
            //    memoryPointer += chunkSize;

            //    for (int i = 4; i < chunkSize-4; i += 1)
            //    {
            //        var thisFloat = BitConverter.ToSingle(bytes, i);

            //        if (Math.Abs(thisFloat - 4185.23) < 1)
            //        {
            //            Console.WriteLine(thisFloat + "\n");
            //        }
            //    }
            //}
            return floats;
        }

        //     public static int LocateInstance()
        //     {

        //         Process process = Process.GetProcessesByName("rocketleague")[0];
        //         IntPtr hProcess1 = MemoryEdits.OpenProcess(16, false, process.Id);

        //         int hProcess2 = (int)hProcess1;
        //long memorySize = process.WorkingSet64;
        //         int baseAddress = (int)process.MainModule.BaseAddress;


        //         byte[] lpBuffer = new byte[memorySize]; ;
        //         int Length = lpBuffer.Length;
        //         // ISSUE: explicit reference operation
        //         // ISSUE: variable of a reference type
        //         var x = ReadFloat(0x32264668);
        //         var bytes = ReadMemory(baseAddress, Length);
        //         //FindFloatSequenceInMemory(new float[] { 889712, 0.00f }, 0);


        //         //for(int i= 374777156; i < memorySize; i+=4)
        //         //{
        //         //    var f = ReadFloat((int)(process.MainModule.BaseAddress + i));
        //         //    if(Math.Abs(f - 156.83) < .1)
        //         //    {
        //         //        Console.WriteLine(f + "\n");
        //         //    }
        //         //}
        ////         foreach (var e in process.Modules)
        ////{
        ////	Console.Write(e + " \n");
        ////}


        // //        int lpNumberOfBytesRead = 0;
        // //        MemoryEdits.ReadProcessMemory(hProcess2, (int) process.MainModule.BaseAddress, lpBuffer, Length, ref lpNumberOfBytesRead);
        // //        MemoryEdits.CloseHandle(hProcess1);

        // //        float[] floats = new float[Length / 4];
        // //        Dictionary<int, float> potentialStarts = new Dictionary<int, float>();

        // //        for(int i=4; i < floats.Length; i+=1)
        // //        {
        // //            var thisFloat = BitConverter.ToSingle(lpBuffer, (int)i);
        // //            var previousFloat = BitConverter.ToSingle(lpBuffer, (int)i-4);
        // //            floats[i/4] = BitConverter.ToSingle(lpBuffer, (int)i);

        //	////if (thisFloat > 17.66f && thisFloat < 17.7f )
        //	//if (thisFloat > 3007.9f  && thisFloat < 3007.94)
        //	//{
        //	//	Console.Write(i + (int)process.MainModule.BaseAddress + " " + thisFloat + "\n");
        //	//	potentialStarts.Add(i + (int)process.MainModule.BaseAddress, thisFloat);
        // //            }
        // //        }

        //     }
    }
}
