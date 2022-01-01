using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MvM.Uploader.Backend.Web.Models
{
    public class EpisodeModel
    {
        public string Code { get; set; }
        [StoreInDatabase]
        public string PodcastTitle { get; set; }
        [StoreInDatabase]
        public string Description { get; set; }
        [StoreInDatabase]
        public List<ParticipantModel> Participants { get; set; } 

    }
}