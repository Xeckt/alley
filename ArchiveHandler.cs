using System.IO;

namespace ModManager
{
    public static class ArchiveHandler
    {
        public static Mod ExtractMod(string archivePath)
        {
            // TODO: Add extraction logic, and check that files are valid inside before placing them into the observable collection
            // Not sure if it's better to use a library for this.
            var fileInfo = new FileInfo(archivePath);

            return new Mod
            {
                Name = Path.GetFileNameWithoutExtension(archivePath),
                Size = Math.Round(fileInfo.Length / (1024.0 * 1024.0), 2), // Convert to MB
                Type = Path.GetExtension(archivePath).ToUpper()
            };
        }
    }
}
