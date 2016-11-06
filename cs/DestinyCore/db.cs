using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Windows;

namespace DestinyCore
{
    class db
    {

        public string en_Assets;
        public string fr_Assets;
        public string es_Assets;
        public string de_Assets;
        public string it_Assets;
        public string ja_Assets;

        static config cfg = new config();
        string apiKey = cfg.apiKey;

        public void getDB(string language)
        {
            Console.WriteLine("Checking for database updates...");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

            var response = client.GetAsync("https://www.bungie.net/platform/Destiny/Manifest/").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            dynamic data = JsonConvert.DeserializeObject(content);


            // Sets variable for each language of the assets
            en_Assets = "https://bungie.net" + data.Response.mobileWorldContentPaths.en;
            fr_Assets = "https://bungie.net" + data.Response.mobileWorldContentPaths.fr;
            es_Assets = "https://bungie.net" + data.Response.mobileWorldContentPaths.es;
            de_Assets = "https://bungie.net" + data.Response.mobileWorldContentPaths.de;
            it_Assets = "https://bungie.net" + data.Response.mobileWorldContentPaths.it;
            ja_Assets = "https://bungie.net" + data.Response.mobileWorldContentPaths.ja;


            // Creates AppData folder for application, sets variable for asset file
            cfg.appData();
            string dbFile = cfg.dbAppdata + language + "_assets" + ".db";


            // Downloads asset file
            WebClient webcli = new WebClient();
            webcli.DownloadFile(new Uri(en_Assets), dbFile);
            unzipDB(language);
            if (cfg.caching == false)
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(cfg.clearCache);
            
        }

        private void unzipDB(string language)
        {
            Console.WriteLine("Unzipping database... (Language: " + language + ")");
            if (!File.Exists(cfg.dbAppdata + language + "_assets.zip"))
                System.IO.File.Move(cfg.dbAppdata + language + "_assets" + ".db", cfg.dbAppdata + language + "_assets.zip");
            ZipFile.ExtractToDirectory(cfg.dbAppdata + language + "_assets" + ".zip", cfg.dbAppdata);
            File.Delete(cfg.dbAppdata + language + "_assets" + ".db");
        }

        private void refreshDB(string language)
        {
            File.Delete(cfg.dbAppdata + en_Assets.Replace("https://bungie.net/common/destiny_content/sqlite/" + cfg.language + "/", ""));
            getDB(language);
        }

    }
}
