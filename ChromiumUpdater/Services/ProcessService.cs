using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ChromiumUpdater.Services
{
    public static class ProcessService
    {
        public static void ShutdownChrome()
        {
            var processes = Process.GetProcesses()
                .Where(x => x.Id != 0 && x.ProcessName != "System" && x.MainModule.ModuleName == "chrome.exe").ToList();

            processes.ForEach(p => p.CloseMainWindow());
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
