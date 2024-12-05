using System.IO;
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
            List<Mod> savedMods = LoadMods();

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
                if (!Mod.ModsList.Any(existingMod => existingMod.Name == mod.Name)) // TODO - find an event driven way to add mods to the list and remove them.
                {
                    Mod.ModsList.Add(mod);
                }
            }
        }

        public static void DeleteMods(IEnumerable<Mod> mods)
        {
            List<Mod> savedMods = LoadMods();

            foreach (var mod in mods)
            {
                int index = savedMods.FindIndex(m => m.Name == mod.Name);
                if (index != 1)
                {
                    savedMods.RemoveAt(index);
                }
            }

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(new ModCache { Mods = savedMods }, Newtonsoft.Json.Formatting.Indented));

        }

        private class ModCache
        {
            public List<Mod>? Mods { get; set; }
        }
    }
}
