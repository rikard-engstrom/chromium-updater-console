using System.Diagnostics;
using System.IO;

namespace ChromiumUpdater.Services
{
    public static class LocalVersionService
    {
        public static string GetChromeVersion()
        {
            try
            {
                return FileVersionInfo.GetVersionInfo(@".\bin\chrome.exe").FileVersion;
            }
            catch (FileNotFoundException)
            {
                return "not-found";
            }
        }
    }
}
