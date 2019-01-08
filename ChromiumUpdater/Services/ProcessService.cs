using System.Diagnostics;
using System.IO;

namespace ChromiumUpdater.Services
{
    public static class ProcessService
    {
        public static void ShutdownChrome()
        {
            var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                try
                {
                    if (process.Id != 0 && process.ProcessName != "System" && process.MainModule.ModuleName == "chrome.exe")
                    {
                        process.CloseMainWindow();
                    }
                }
                catch { }
            }
        }

        public static void LaunchChrome()
        {
            Process.Start(new ProcessStartInfo
            {
                WorkingDirectory = Path.GetFullPath(".\\bin"),
                FileName = Path.GetFullPath(".\\bin\\chrome.exe")
            });
        }
    }
}
