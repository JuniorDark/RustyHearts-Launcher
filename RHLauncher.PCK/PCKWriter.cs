using Newtonsoft.Json;
using RHLauncher.DynamicLib;
using RHLauncher.Helper;
using System.Text;

namespace RHLauncher.PCK
{
    public static class PCKWriter
    {
        private static readonly byte[] BufferTable = new byte[] {
                0x30, 0x22, 0x41, 0xa8, 0x5b, 0xa6, 0x6a, 0x49, 0xbf, 0x53, 0x35, 0xe5, 0x9e, 14, 0xec, 0xb8,
                0x5e, 0x15, 0x1f, 0xc1, 0x4f, 0xec, 0x77, 0xe8, 0xb7, 0x4e, 0x87, 230, 0xf5, 60, 0xb3, 0x43,
                0xcc, 0x53, 0x36, 0xac, 90, 0x77, 0xb8, 0xdd, 0x30, 0x74, 140, 0x4a, 0x9a, 0x9b, 0xbc, 10,
                0xa4, 0xad, 0xbb, 0x13, 0x4b, 140, 0xd4, 0x80, 0xce, 0x65, 0x1d, 8, 90, 0x6a, 0x6f, 0x25,
                0xf9, 0x3f, 0xef, 0x1b, 0xa4, 0x72, 20, 0xed, 0x97, 0x22, 0x4a, 0x2e, 0xb8, 150, 0x4b, 0x8e,
                150, 0x93, 0xf1, 40, 0xb2, 11, 60, 0xf8, 0x5d, 170, 0xa9, 130, 0x13, 110, 0xc1, 0xa9,
                0x20, 0x57, 0xb2, 0x5b, 0x16, 0xcf, 0x9e, 0x5f, 0xd4, 0xcc, 0x2e, 0xf5, 0xc9, 0x4c, 0x1c, 0xee,
                0xe3, 0x3f, 0x29, 0xb3, 6, 0x70, 0x43, 0x3d, 0xf5, 0x90, 0xa2, 0x42, 2, 0x98, 80, 0xfd,
                0x5d, 0x4e, 0x92, 0xad, 0xad, 0x7f, 0xab, 0x60, 0x2c, 0xb8, 0x43, 0x76, 0x8f, 0x5f, 230, 0xa7,
                0x19, 0xe0, 0xb9, 0xb5, 0x62, 0x6b, 0xd4, 0x47, 0x69, 0x34, 14, 0x6d, 0xa4, 0x52, 0xe3, 100,
                0x4a, 0x65, 0x47, 0xf5, 0x3f, 0x53, 0x5e, 0x8b, 0x1b, 0xfd, 0x21, 0xf7, 0xba, 0x68, 0xf9, 0xdf,
                0x68, 0xa8, 150, 15, 0x8b, 1, 0x97, 0x58, 140, 30, 0xef, 0xb3, 0x41, 0x44, 0x21, 0xda,
                0xe0, 0xf4, 0xe0, 0x2d, 0xcd, 11, 240, 0x5c, 0x59, 0xd6, 0x99, 0xe7, 1, 0x15, 0x67, 50,
                0xe0, 0x12, 0x2f, 0xcd, 0xa2, 0xde, 0x52, 0xce, 0xec, 0xef, 0x77, 14, 0xbc, 0x38, 100, 0x8d,
                180, 0xdb, 0x67, 0xff, 200, 0x66, 12, 0x8a, 0x60, 0xe1, 0x2e, 0, 0x43, 0xa9, 0x37, 0x9c,
                0x11, 170, 0xb9, 0x98, 0xed, 0x21, 0x35, 0xd4, 0xc3, 0xde, 0x65, 0x54, 0x9d, 0x1c, 0xb0, 0xa9
             };
        private static readonly uint[] CodeHash = new uint[]
        {
        0, 0x77073096, 0xee0e612c, 0x990951ba, 0x76dc419, 0x706af48f, 0xe963a535, 0x9e6495a3, 0xedb8832, 0x79dcb8a4, 0xe0d5e91e, 0x97d2d988, 0x9b64c2b, 0x7eb17cbd, 0xe7b82d07, 0x90bf1d91,
        0x1db71064, 0x6ab020f2, 0xf3b97148, 0x84be41de, 0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7, 0x136c9856, 0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9, 0xfa0f3d63, 0x8d080df5,
        0x3b6e20c8, 0x4c69105e, 0xd56041e4, 0xa2677172, 0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b, 0x35b5a8fa, 0x42b2986c, 0xdbbbc9d6, 0xacbcf940, 0x32d86ce3, 0x45df5c75, 0xdcd60dcf, 0xabd13d59,
        0x26d930ac, 0x51de003a, 0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423, 0xcfba9599, 0xb8bda50f, 0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924, 0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d,
        0x76dc4190, 0x1db7106, 0x98d220bc, 0xefd5102a, 0x71b18589, 0x6b6b51f, 0x9fbfe4a5, 0xe8b8d433, 0x7807c9a2, 0xf00f934, 0x9609a88e, 0xe10e9818, 0x7f6a0dbb, 0x86d3d2d, 0x91646c97, 0xe6635c01,
        0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e, 0x6c0695ed, 0x1b01a57b, 0x8208f4c1, 0xf50fc457, 0x65b0d9c6, 0x12b7e950, 0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3, 0xfbd44c65,
        0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2, 0x4adfa541, 0x3dd895d7, 0xa4d1c46d, 0xd3d6f4fb, 0x4369e96a, 0x346ed9fc, 0xad678846, 0xda60b8d0, 0x44042d73, 0x33031de5, 0xaa0a4c5f, 0xdd0d7cc9,
        0x5005713c, 0x270241aa, 0xbe0b1010, 0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f, 0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17, 0x2eb40d81, 0xb7bd5c3b, 0xc0ba6cad,
        0xedb88320, 0x9abfb3b6, 0x3b6e20c, 0x74b1d29a, 0xead54739, 0x9dd277af, 0x4db2615, 0x73dc1683, 0xe3630b12, 0x94643b84, 0xd6d6a3e, 0x7a6a5aa8, 0xe40ecf0b, 0x9309ff9d, 0xa00ae27, 0x7d079eb1,
        0xf00f9344, 0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb, 0x196c3671, 0x6e6b06e7, 0xfed41b76, 0x89d32be0, 0x10da7a5a, 0x67dd4acc, 0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5,
        0xd6d6a3e8, 0xa1d1937e, 0x38d8c2c4, 0x4fdff252, 0xd1bb67f1, 0xa6bc5767, 0x3fb506dd, 0x48b2364b, 0xd80d2bda, 0xaf0a1b4c, 0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55, 0x316e8eef, 0x4669be79,
        0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236, 0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f, 0xc5ba3bbe, 0xb2bd0b28, 0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31, 0x2cd99e8b, 0x5bdeae1d,
        0x9b64c2b0, 0xec63f226, 0x756aa39c, 0x26d930a, 0x9c0906a9, 0xeb0e363f, 0x72076785, 0x5005713, 0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0xcb61b38, 0x92d28e9b, 0xe5d5be0d, 0x7cdcefb7, 0xbdbdf21,
        0x86d3d2d4, 0xf1d4e242, 0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1, 0x18b74777, 0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c, 0x8f659eff, 0xf862ae69, 0x616bffd3, 0x166ccf45,
        0xa00ae278, 0xd70dd2ee, 0x4e048354, 0x3903b3c2, 0xa7672661, 0xd06016f7, 0x4969474d, 0x3e6e77db, 0xaed16a4a, 0xd9d65adc, 0x40df0b66, 0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9,
        0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605, 0xcdd70693, 0x54de5729, 0x23d967bf, 0xb3667a2e, 0xc4614ab8, 0x5d681b02, 0x2a6f2b94, 0xb40bbe37, 0xc30c8ea1, 0x5a05df1b, 0x2d02ef8d
        };

        public static uint BytesWithCodeHash(byte[] toBytes)
        {
            int num = -1;
            for (int i = 0; i < toBytes.Length; i++)
            {
                num = (int)CodeHash[num & 0xff ^ toBytes[i]] ^ num >> 8 & 0xffffff;
            }
            return (uint)num;
        }

        public delegate void GetFilesDelegate(string path, bool isComplete);
        public static List<string> GetFiles(string dir, GetFilesDelegate gfDelegate)
        {
            List<string> listFile = new();

            foreach (string file in Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories))
            {
                if (File.GetAttributes(file).HasFlag(FileAttributes.Directory))
                {
                    continue;
                }

                listFile.Add(file);
                gfDelegate(file, false);
            }

            return listFile;
        }

        public delegate void PackingDelegate(string fileName, int pos, int count);
        public static async Task Packing(string rootDir, List<string> listFile, List<PCKFile> listPckFile, bool replace, PackingDelegate pDelegate, CancellationToken cancellationToken)
        {

            RegistryHandler registryHandler = new();
            string installDirectory = registryHandler.GetInstallDirectory();

            SortedDictionary<string, PCKFile> dicPckFile = new();
            foreach (PCKFile file in listPckFile)
            {
                dicPckFile.Add(file.Name, file);
            }

            SortedDictionary<string, string> dicInputFile = new();
            string s = rootDir.Substring(rootDir.Length - 1, 1);
            if (s != "\\") rootDir += "\\";
            foreach (string file in listFile)
            {
                dicInputFile.Add(file.Replace(rootDir, ""), file);
            }

            Dictionary<int, FileStream> archives = new(10);
            Dictionary<int, BinaryWriter> writers = new(10);
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    string pathPCK = Path.Combine(installDirectory, string.Format("{0:000}.pck", i));
                    FileMode mode = File.Exists(pathPCK) ? FileMode.Open : FileMode.Create;
                    FileStream ofs = new(pathPCK, mode, FileAccess.Write);
                    ofs.Position = ofs.Length;
                    BinaryWriter obr = new(ofs);
                    archives.Add(i, ofs);
                    writers.Add(i, obr);
                }

                // Read the f00x.dat file to get the archive numbers
                Dictionary<string, int> dicArchive = new();
                string pathF00X = Path.Combine(installDirectory, "f00x.dat");
                if (File.Exists(pathF00X))
                {
                    byte[] buffer = await File.ReadAllBytesAsync(pathF00X, cancellationToken);
                    int numDecompressed = buffer.Length << 4;
                    byte[] decompressed = new byte[numDecompressed];
                    int result = ZLibDll.Decompress(buffer, buffer.Length, decompressed, ref numDecompressed);
                    if (result == 0)
                    {
                        string json = Encoding.UTF8.GetString(decompressed);
                        dicArchive = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
                    }
                }

                int pos = 0;
                int count = dicInputFile.Count;
                foreach (KeyValuePair<string, string> file in dicInputFile)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        WriteF00XDAT(dicPckFile);
                        throw new OperationCanceledException();
                    }

                    pos++;
                    pDelegate(file.Key, pos, count);
                    PCKFile? pckFile = null;
                    if (dicPckFile.ContainsKey(file.Key))
                    {
                        if (!replace) continue;
                        pckFile = dicPckFile[file.Key];
                    }

                    if (!File.Exists(file.Value)) continue;

                    byte[] fileBytes;
                    using (FileStream fileStream = File.OpenRead(file.Value))
                    {
                        fileStream.Seek(0, SeekOrigin.Begin);
                        fileBytes = new byte[fileStream.Length];
                        await fileStream.ReadAsync(fileBytes, 0, fileBytes.Length);
                    }

                    uint fileHash = BytesWithCodeHash(fileBytes);
                    int arc = 0;
                    if (pckFile != null)
                    {
                        if (pckFile.Hash == fileHash) continue;
                        arc = pckFile.Archive;
                        dicPckFile.Remove(file.Key);
                    }
                    else
                    {
                        if (dicArchive.ContainsKey(file.Key)) arc = dicArchive[file.Key];
                        else
                        {
                            long pckLen = archives[0].Length;

                            for (byte i = 1; i < 10; i++)
                            {
                                long x = archives[i].Length;
                                if (x < pckLen)
                                {
                                    pckLen = x;
                                    arc = i;
                                }
                            }
                        }
                    }

                    pckFile = new PCKFile(file.Key, (byte)arc, fileBytes.Length, fileHash, archives[arc].Length);
                    dicPckFile.Add(file.Key, pckFile);
                    BinaryWriter bw = writers[pckFile.Archive];
                    bw.Write(fileBytes);
                    bw.Flush();
                }
                WriteF00XDAT(dicPckFile);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                foreach (KeyValuePair<int, FileStream> kvp in archives)
                {
                    kvp.Value.Close();
                    kvp.Value.Dispose();
                }

                listFile.Clear();
                listPckFile.Clear();
            }

        }

        public static void WriteF00XDAT(SortedDictionary<string, PCKFile> dicPckFile)
        {
            try
            {
                RegistryHandler registryHandler = new();
                string installDirectory = registryHandler.GetInstallDirectory();
                byte[]? bufferF00XDAT = null;
                using (MemoryStream streamF00X = new())
                {
                    using BinaryWriter writerF00X = new(streamF00X);
                    foreach (KeyValuePair<string, PCKFile> kvFile in dicPckFile)
                    {
                        PCKFile pckFile = kvFile.Value;

                        byte[] bytes = Encoding.Unicode.GetBytes(pckFile.Name);
                        int numNameLen = Convert.ToInt32(bytes.Length / 2);
                        writerF00X.Write((ushort)numNameLen);
                        writerF00X.Write(bytes);
                        writerF00X.Write(pckFile.Archive);
                        writerF00X.Write((uint)pckFile.FileSize);
                        writerF00X.Write(pckFile.Hash);
                        writerF00X.Write((ulong)pckFile.Offset);
                    }
                    writerF00X.Flush();
                    bufferF00XDAT = streamF00X.ToArray();
                }

                int numCompress = (bufferF00XDAT.Length >> 4) + bufferF00XDAT.Length;
                byte[] bufferCompress = new byte[numCompress];
                if (ZLibDll.Compress(bufferF00XDAT, bufferF00XDAT.Length, bufferCompress, ref numCompress) == 0)
                {
                    for (int i = 0; i < numCompress; i++)
                    {
                        bufferCompress[i] = (byte)(bufferCompress[i] ^ BufferTable[i & 0xff]);
                    }

                    string pathF00XDAT = Path.Combine(installDirectory, "f00X.dat");
                    File.Delete(pathF00XDAT + ".old");
                    File.Move(pathF00XDAT, pathF00XDAT + ".old");

                    using FileStream streamF00XDAT = new(pathF00XDAT, FileMode.Create, FileAccess.Write);
                    using BinaryWriter writerF00XDAT = new(streamF00XDAT);
                    writerF00XDAT.Write(bufferCompress, 0, numCompress);
                    writerF00XDAT.Flush();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
