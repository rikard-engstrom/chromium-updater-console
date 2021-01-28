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
            var chromeExe = args.Length == 1 ? args[0] : "chrome.exe";

            var installed = LocalVersionService.GetChromeVersion();
            Console.WriteLine($"Installed version {installed}");

            Console.WriteLine("Looking for available version");
            var available = AvailableVersionService.LatestWindos64();

            if (available.Version != installed)
            {
                Update(available, chromeExe);
            }
            else
            {
                Console.WriteLine($"Your installed version {installed}, is the latest.");
            }
        }

        static void Update(AvailableVersion available, string chromeExe)
        {
            Console.WriteLine($"Downloading version {available.Version}");
            DownloadService.DownloadChrome(available.Download, available.Version);

            Console.WriteLine($"Extracting");
            ZipService.Unzip($"chrome-{available.Version}.zip", ".\\temp");

            Console.WriteLine("Shutting down Chrome");
            ProcessService.ShutdownChrome(chromeExe);

            DeleteDirectoryIfExists(".\\bin");

            if (Directory.Exists(".\\temp\\chrome-win32"))
                Directory.Move(".\\temp\\chrome-win32", ".\\bin");

            if (Directory.Exists(".\\temp\\chrome-win"))
                Directory.Move(".\\temp\\chrome-win", ".\\bin");

            if (chromeExe != "chrome.exe")
                File.Move(".\\bin\\chrome.exe", $".\\bin\\{chromeExe}");

            ProcessService.LaunchChrome(chromeExe);
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