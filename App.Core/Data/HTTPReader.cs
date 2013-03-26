using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace App.Core.Data
{
    public static class HTTPReader
    {
        public static string Load(string address)
        {
            var request = WebRequest.Create(address) as HttpWebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }
        }

        public static async Task<string> LoadAsync(string address)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(address);
                response.EnsureSuccessStatusCode();
                return (await response.Content.ReadAsStringAsync());
            }
        }

    }
}
