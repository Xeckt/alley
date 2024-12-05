using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace ModManager.Handler
{
    public static class CacheHandler
    {
        // Path to the JSON file that stores the mod cache
        private static readonly string _filePath = "modcache.json";

        /// Loads the mods from the JSON file
        /// If the file doesn't exist, it returns an empty list
        public static List<Mod> LoadMods()
        {
            // Check if the JSON file exists
            if (!File.Exists(_filePath))
            {
                return new List<Mod>(); // Return an empty list if no file exists
            }

            // Read the JSON content from the file
            var json = File.ReadAllText(_filePath);

            // Deserialize the JSON content into a ModCache object
            var cache = JsonConvert.DeserializeObject<ModCache>(json);

            // Return the list of mods or an empty list if null
            return cache?.Mods ?? new List<Mod>();
        }

        /// Saves the given list of mods to the JSON file
        /// If it exists, it will overwritte
        public static void SaveMods(IEnumerable<Mod> mods)
        {
            // Wrap the list of mods in a ModCache object
            var cache = new ModCache { Mods = mods.ToList() };

            // Serialize the ModCache object into a JSON string
            var json = JsonConvert.SerializeObject(cache, Newtonsoft.Json.Formatting.Indented);

            // Write the JSON string to the file
            File.AppendAllText(_filePath, json);

            foreach (var mod in mods) {
                if (!Mod.ModsList.Any(existingMod => existingMod.Name == mod.Name)) // Check if the same mod is currently loaded in the list
                { 
                    Mod.ModsList.Add(mod);
                }
            }
        }

        /// Private class to wrap the mods for JSON
        /// It ensures the structure matches the JSON file format
        private class ModCache
        {
            public List<Mod>? Mods { get; set; }
        }
    }
}
