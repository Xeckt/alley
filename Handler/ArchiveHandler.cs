using System.Diagnostics;
using System.IO;
using System.Windows;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace ModManager.Handler
{
    public static class ArchiveHandler
    {
        public static Mod ExtractMod(string archivePath)
        {
            var fileInfo = new FileInfo(archivePath);

            try
            {
                // Open the archive
                using (var archive = ArchiveFactory.Open(archivePath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            Trace.WriteLine($"Extracting: {entry.Key}");

                            entry.WriteToDirectory("extractiontest", new ExtractionOptions
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                }

            } catch (Exception ex)
            {
                MessageBox.Show($"Error extracting archive: {ex.Message}");
            }

            return new Mod

            {};
        }
    }
}
