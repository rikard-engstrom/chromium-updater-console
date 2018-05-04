using System.IO;
using System.IO.Compression;

namespace ChromiumUpdater.Services
{
    public static class ZipService
    {
        public static void Unzip(string archive, string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, recursive: true);
            }

            using (var stream = new FileStream(archive, FileMode.Open))
            using (var zip = new ZipArchive(stream))
            {
                zip.ExtractToDirectory(folder);
            }
        }
    }
}
