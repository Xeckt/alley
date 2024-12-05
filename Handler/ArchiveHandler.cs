using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
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
                using (var archive = ArchiveFactory.Open(archivePath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        string modName = Path.GetFileNameWithoutExtension(archivePath);
                        string fullModPath = Path.Combine(ExtractPath, entry.Key);

                        var mod = modList.FirstOrDefault(m => m.Name == modName);

                        if (mod == null)
                        {
                            mod = new Mod
                            {
                                Type = ModImportWindow.GetSelectedType(),
                                Name = modName,
                                Size = Math.Round(fileInfo.Length / (1024.0 * 1024.0), 2),
                                IsEnabled = false
                            };
                        }

                        if (!entry.IsDirectory)
                        {
                            Trace.WriteLine($"Extracting: {entry.Key}");

                            entry.WriteToDirectory(ExtractPath, new ExtractionOptions
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });

                            string checksum = GetFileChecksum(fullModPath, SHA256.Create());

                            mod.Files.Add(new ModFiles 
                            {
                                FileName = fullModPath,
                                Checksum = checksum,
                            });

                            modList.Add(mod);
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

        private static string GetFileChecksum(string filePath, HashAlgorithm hashAlgorithm) 
        {
            using (hashAlgorithm)
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = hashAlgorithm.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
