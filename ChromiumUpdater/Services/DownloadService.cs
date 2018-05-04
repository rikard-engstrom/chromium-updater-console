using System.IO;
using System.Net;

namespace ChromiumUpdater.Services
{
    public static class DownloadService
    {
        public static void DownloadChrome(string url, string version)
        {
            if (File.Exists($"chrome-{version}.zip"))
            {
                return;
            }

            var request = WebRequest.CreateHttp(url);

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var fileStream = new FileStream($"chrome-{version}.zip", FileMode.Create))
            {
                responseStream.CopyTo(fileStream);
            }
        }
    }
}
