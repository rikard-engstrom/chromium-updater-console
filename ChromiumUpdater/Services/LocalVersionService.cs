using System.Diagnostics;
using System.IO;

namespace ChromiumUpdater.Services
{
    public static class LocalVersionService
    {
        public static string GetChromeVersion(string chromeExe = null)
        {
            chromeExe = chromeExe ?? "chrome.exe";
            try
            {
                return FileVersionInfo.GetVersionInfo($@".\bin\{chromeExe}").FileVersion;
            }
            catch (FileNotFoundException)
            {
                return "not-found";
            }
        }
    }
}
