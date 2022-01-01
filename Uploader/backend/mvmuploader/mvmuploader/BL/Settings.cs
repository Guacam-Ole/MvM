

using Microsoft.AspNetCore.Hosting;
using MvM.Uploader.Backend.Web.Models;

using Newtonsoft.Json;
using System.IO;

namespace MvM.Uploader.Backend.Web.BL
{
    public class Settings
    {
        private const string SecretsFile = "Properties/secrets.json";
        private const string ConfigFile = "Properties/config.json";

        public readonly Secrets Secrets;
        public readonly Config Config;

        public Settings(IWebHostEnvironment hostEnvironment)
        {
            //string absolutePath = Path.Combine(hostEnvironment.ContentRootPath, SecretsFile);
            string secretContents = File.ReadAllText(SecretsFile);
            string configContents = File.ReadAllText(ConfigFile);


            Secrets = JsonConvert.DeserializeObject<Secrets>(secretContents);
            Config=JsonConvert.DeserializeObject<Config>(configContents);   
        }
    }
}
