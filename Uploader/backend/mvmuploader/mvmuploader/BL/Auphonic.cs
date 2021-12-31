using Microsoft.AspNetCore.Http;
using MvM.Uploader.Backend.Web.Models;
using MvM.Uploader.Backend.Web.Models.Auphonic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Models = MvM.Uploader.Backend.Web.Models;

namespace MvM.Uploader.Backend.Web.BL
{
    public class Auphonic
    {
        private readonly Settings _settings;
        private readonly Authentication _authentication;

        public Auphonic( Settings settings, Authentication authentication)
        {
            _settings = settings;
            _authentication = authentication;
        }

        public async Task<AuphonicAccount> GetAccountAsync()    
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("GET"), "https://auphonic.com/api/user.json");

            _authentication.AddBasicAuthToRequest(request, _settings.Secrets.Auphonic.Username, _settings.Secrets.Auphonic.Password);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuphonicWrapper<AuphonicAccount>>(content).Data;
        }

        private string BuildContent(string preset, Dictionary<string,string> metadata)
        {
            string content = $"{{ \"preset\":\"{preset}\",";
            content+=BuildMetaDataContent(metadata);
            content += "}\n";
            return content;
        }

        private string BuildMetaDataContent(Dictionary<string, string> metadata)
        {
            string metadataContent = "\"metadata\": {";
            foreach (var pair in metadata)
            {
                metadataContent += $"\"{pair.Key}\":\"{pair.Value}\",";
            }
            metadataContent += "}\n";
            return metadataContent;
        }

        public async Task<string> CreateProductionFromPreset( EpisodeModel uploadModel)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), "https://auphonic.com/api/productions.json");
            _authentication.AddBasicAuthToRequest(request, _settings.Secrets.Auphonic.Username, _settings.Secrets.Auphonic.Password);

            var metaData=new Dictionary<string, string>();
            metaData.Add("title", uploadModel.PodcastTitle); // TODO: More Stuff

            request.Content = new StringContent(BuildContent(_settings.Config.Auphonic.Preset, metaData));
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response=await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var data =JsonConvert.DeserializeObject<AuphonicWrapper<AuphonicProduction>>(responseContent).Data;
            string ssid = data.Ssid;
            return ssid;
        }

        public  void UploadFileToProduction(string production, IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
             UploadFileToProduction(production, ms.ToArray());
        }

        private async void UploadFileToProduction(string production, byte[] filecontent)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://auphonic.com/api/production/{production}/upload.json");
            _authentication.AddBasicAuthToRequest(request, _settings.Secrets.Auphonic.Username, _settings.Secrets.Auphonic.Password);
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new ByteArrayContent(filecontent), "input_file", "episode.mp3");
            request.Content = multipartContent;

            var response = await client.SendAsync(request);
        }

        public async void StartProduction(string production)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(new HttpMethod("POST"), $"https://auphonic.com/api/production/{production}/start.json");
            var response = await client.SendAsync(request);
        }
    }
}
