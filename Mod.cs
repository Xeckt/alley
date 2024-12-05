using System.Collections.ObjectModel;

namespace ModManager
{
    public class Mod
    {
        public string Name { get; set; }
        public double Size { get; set; } // Size in MB
        public ModType Type { get; set; }
        public bool IsEnabled { get; set; } = false;
        public List<ModFiles> Files { get; set; } = new List<ModFiles>();
        public static ObservableCollection<Mod> ModsList { get; set; }
    }

    public class ModFiles 
    { 
        public string FileName { get; set; }
        public string Checksum { get; set; }
    }

    public enum ModType 
    {
        Map = 1,
        Clothing = 2,
        Animation = 3,
        Utility = 4,
        Blueprint = 5,
        Custom = 6,
        Unknown = 7,
    }
}