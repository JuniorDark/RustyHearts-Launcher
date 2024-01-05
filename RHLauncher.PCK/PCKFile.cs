namespace RHLauncher.RHLauncher.PCK
{
    public class PCKFile
    {
        public string Name { get; private set; }

        public byte Archive { get; private set; }

        public int FileSize { get; private set; }

        public uint Hash { get; private set; }

        public long Offset { get; private set; }

        public PCKFile(string name, byte archive, int size, uint hash, long offset)
        {
            Name = name;
            Archive = archive;
            FileSize = size;
            Hash = hash;
            Offset = offset;
        }

    }
}
