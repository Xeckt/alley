using System.IO;

namespace ModManager.Handler
{
    public static class ArchiveHandler
    {
        public static Mod ExtractMod(string archivePath)
        {
            // TODO: Add extraction logic, and check that files are valid inside before placing them into the observable collection
            // Not sure if it's better to use a library for this.
            var fileInfo = new FileInfo(archivePath);

            return new Mod

            {};
        }
    }
}
