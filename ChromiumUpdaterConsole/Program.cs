using ChromiumUpdater.Models;
using ChromiumUpdater.Services;
using System;
using System.IO;

namespace ChromiumUpdaterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var installed = LocalVersionService.GetChromeVersion();
            Console.WriteLine($"Installed version {installed}");

            Console.WriteLine("Looking for available version");
            var available = AvailableVersionService.LatestWindos64();

            if (available.Version != installed)
            {
                Update(available);
            }
            else
            {
                Console.WriteLine($"Your installed version {installed}, is the latest.");
            }
        }

        static void Update(AvailableVersion available)
        {
            Console.WriteLine($"Downloading version {available.Version}");
            DownloadService.DownloadChrome(available.Download, available.Version);

            Console.WriteLine($"Extracting");
            ZipService.Unzip($"chrome-{available.Version}.zip", ".\\temp");

            Console.WriteLine("Shutting down Chrome");
            ProcessService.ShutdownChrome();

            DeleteDirectoryIfExists(".\\bin");
            Directory.Move(".\\temp\\chrome-win32", ".\\bin");

            ProcessService.LaunchChrome();
        }

        static void DeleteDirectoryIfExists(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(".\\bin", recursive: true);
            }
        }
    }
}