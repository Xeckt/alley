using System.Collections.ObjectModel;

namespace ModManager
{
    public class Mod
    {
        public string Name { get; set; }
        public double Size { get; set; } // Size in MB
        public ModType Type { get; set; }
        public bool IsEnabled { get; set; } = false;
        public List<string> FilePaths { get; set; } = new List<string>();
        public static ObservableCollection<Mod> ModsList { get; set; }
    }

    public enum ModType 
    {
        Map,
        Clothing,
        Utility,
        Custom
    }
}