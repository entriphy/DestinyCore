using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace DestinyCore
{
    class Items
    {
        config cfg = new config();
        HttpClient client = new HttpClient();
        public string hashToItem(String hash)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-Key", cfg.apiKey);

            var response = client.GetAsync("https://www.bungie.net/platform/Destiny/Manifest/InventoryItem/" + hash).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(content);

            return item.Response.data.inventoryItem.itemName;
        }

        public string getItemIcon(String hash)
        {
            client.DefaultRequestHeaders.Add("X-API-Key", cfg.apiKey);

            var response = client.GetAsync("https://www.bungie.net/platform/Destiny/Manifest/InventoryItem/" + hash).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(content);
            string result = "https://bungie.net" + item.Response.data.inventoryItem.icon;



            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\DestinyCore\cache\";
            string temp = appdata + hashToItem(hash) + ".jpg";

            if (!Directory.Exists(appdata))
                Directory.CreateDirectory(appdata);

            Console.WriteLine(temp);
            WebClient webcli = new WebClient();
            webcli.DownloadFile(new Uri(result), appdata + hashToItem(hash) + ".jpg");

            return result;
        }
    }
}
