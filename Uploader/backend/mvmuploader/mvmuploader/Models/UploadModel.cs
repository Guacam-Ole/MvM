using Microsoft.AspNetCore.Http;

namespace MvM.Uploader.Backend.Web.Models
{
    public class UploadModel
    {
        public string Name { get; set; }    
        public IFormFile File { get; set; } 
        public string FileName { get; set; }    
    }
}
