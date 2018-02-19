using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Newsi.Core
{
    class NewsiAPI
    {
        private HttpClient client;
        private Uri uri
        {
            get
            {
                return new Uri(string.Format("https://newsapi.org/v2/top-headlines?country=fr&apiKey=23fb265112c544a580577d61c31d2c4e"));
            }
        }

        public NewsiAPI()
        {
            client = new HttpClient();
        }

        public async Task<RootObject> Task()
        {
            RootObject data = null;

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<RootObject>(content);
            }

            return data;
        }
    }
}