using Microsoft.AspNetCore.Http;

using System.Collections.Generic;

namespace mvmuploader.Models
{
    public class UploadModel
    {
        public string Code { get; set; }
        public string PodcastTitle { get; set; }
        public string Description { get; set; }
        public IFormFile UploadFile { get; set; }
        public List<ParticipantModel> Participants { get; } = new List<ParticipantModel>();
    }
}