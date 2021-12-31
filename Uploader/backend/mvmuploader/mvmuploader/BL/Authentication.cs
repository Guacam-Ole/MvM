using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace MvM.Uploader.Backend.Web.BL
{
    public class Authentication
    {
        private readonly Settings _settings;
        private readonly Database _database;

        public Authentication(Settings settings, Database database)
        {
            this._settings = settings;
            _database = database;
        }

        public string HashWithSalt(string value)
        {
            string salted = $"{_settings.Secrets.Salt}_{value}";
            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(salted);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public bool AdminPasswordIsValid(string password)
        {
            return HashWithSalt(password).Equals(_settings.Secrets.Admin);
        }

        public bool CodeIsValid(string code)
        {
            string storedCode = _database.ReadUploadCodeHash();
            return code.Equals(storedCode);
        }

        public void AddBasicAuthToRequest(HttpRequestMessage request, string username, string password)
        {
            var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            request.Headers.Add("Authorization", $"Basic {base64authorization}");
        }
    }
}
