namespace RHLauncher.PCK
{
    public class PCKFileNode
    {
        public string Name { get; private set; }
        public PCKFile PCKFile { get; set; }
        public bool IsDir { get { return PCKFile == null; } }
        public SortedDictionary<string, PCKFileNode> Nodes { get; set; }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                if (PCKFile != null) PCKFile.IsChecked = isChecked;
            }
        }

        public PCKFileNode(string name, PCKFile file)
        {
            Name = name;
            PCKFile = file;
        }
    }
}
