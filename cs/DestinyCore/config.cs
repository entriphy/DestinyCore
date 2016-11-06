using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCore
{
    class config
    {
        #region Place API Key here
        public string apiKey = "<API Key here>";
        #endregion

        #region Directories
        public string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\DestinyCore\cache\";
        public string dbAppdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\DestinyCore\cache\db\";
        public System.IO.DirectoryInfo appdataInfo = new DirectoryInfo(Environment.SpecialFolder.ApplicationData + @"\DestinyCore\cache\");


        #endregion
        // Change language here
        // Valid languages: en, fr, es, de, it, ja
        // Language will be used mainly for downloading database
        public string language = "<Language here";
        public bool caching = true;


        HttpClient client = new HttpClient();

        // Creates folder for caching in AppData\Roaming
        public void appData()
        {
            if (!Directory.Exists(appdata))
                Directory.CreateDirectory(appdata);
                Directory.CreateDirectory(dbAppdata);
        }

        public void clearCache(object sender, EventArgs e)
        {
            foreach (FileInfo file in appdataInfo.GetFiles())
            {
                file.Delete();
            }
        }

    }
}
