using System.Diagnostics;
using System.IO;
using System.Windows;
using ModManager.Windows;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace ModManager.Handler
{
    public partial class ArchiveHandler
    {

        public static void ExtractAndImportMod(string archivePath)
        {
            var fileInfo = new FileInfo(archivePath);
            var modList = new List<Mod>();
            string ExtractPath = "extractiontest";

            try
            {
                // Open the archive
                using (var archive = ArchiveFactory.Open(archivePath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        string modName = Path.GetFileNameWithoutExtension(archivePath);
                        var mod = modList.FirstOrDefault(m => m.Name == modName);

                        if (mod == null)
                        {
                            mod = new Mod
                            {
                                Type = ModImportWindow.GetSelectedType(),
                                Name = modName,
                                FilePaths = new List<string>(),
                                Size = Math.Round(fileInfo.Length / (1024.0 * 1024.0), 2),
                                IsEnabled = false
                            };
                            modList.Add(mod);
                        }

                        if (!entry.IsDirectory)
                        {
                            Trace.WriteLine($"Extracting: {entry.Key}");

                            entry.WriteToDirectory(ExtractPath, new ExtractionOptions
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                            mod.FilePaths.Add(Path.Combine(ExtractPath, entry.Key));
                        };

                    }
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Error during extraction: {ex.Message}");
            }
            CacheHandler.SaveMods(modList);
        }
    }
}
