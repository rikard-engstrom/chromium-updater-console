using ChromiumUpdater.Models;
using System.IO;
using System.Linq;
using System.Net;

namespace ChromiumUpdater.Services
{
    public static class AvailableVersionService
    {
        public static AvailableVersion LatestWindos64()
        {
            var versionData = GetVersionFromApi();
            var dictionary = versionData.Split(';')
                                .Select(x => x.Split('='))
                                .ToDictionary(key => key[0], value => value[1]);

            return new AvailableVersion
            {
                Version = dictionary["version"],
                Download = dictionary["download"]
            };
        }

        private static string GetVersionFromApi()
        {
            var request = WebRequest.CreateHttp("https://chromium.woolyss.com/api/v3/?os=windows&bit=64&out=string");

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
