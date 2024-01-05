namespace RHLauncher.RHLauncher.PCK
{
    public class PCKFileNode
    {
        public string Name { get; private set; }
        public PCKFile PCKFile { get; set; }

        public PCKFileNode(string name, PCKFile file)
        {
            Name = name;
            PCKFile = file;
        }
    }
}
