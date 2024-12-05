using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace ModManager.Handler
{
    public static class CacheHandler
    {
        private static readonly string _filePath = "modcache.json";

        public static List<Mod> LoadMods()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Mod>();
            }

            var json = File.ReadAllText(_filePath);

            var cache = JsonConvert.DeserializeObject<ModCache>(json);

            return cache?.Mods ?? new List<Mod>();
        }

        public static void SaveMods(IEnumerable<Mod> mods)
        {
            List<Mod> savedMods;

            if (File.Exists(_filePath))
            {
                var cacheContent = File.ReadAllText(_filePath);
                var savedCache = JsonConvert.DeserializeObject<ModCache>(cacheContent);
                savedMods = savedCache?.Mods ?? new List<Mod>();
            } 
            else
            {
                savedMods = new List<Mod>();
            }

            foreach (var mod in mods)
            {
                if (!savedMods.Exists(m => m.Name == mod.Name))
                {
                    savedMods.Add(mod);
                }
            }

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(new ModCache { Mods = savedMods }, Newtonsoft.Json.Formatting.Indented));

            foreach (var mod in savedMods)
            {
                if (!Mod.ModsList.Any(existingMod => existingMod.Name == mod.Name))
                {
                    Mod.ModsList.Add(mod);
                }
            }
        }

        private class ModCache
        {
            public List<Mod>? Mods { get; set; }
        }
    }
}
