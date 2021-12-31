using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MvM.Uploader.Backend.Web.Models;

namespace MvM.Uploader.Backend.Web.BL
{
    public class Episode
    {
        private EpisodeModel _uploadedEpisode;
        private readonly Database _database;
        private readonly ILogger<Episode> _logger;

        public Episode(Database database, ILogger<Episode> logger)
        {
            _database = database;
            _logger = logger;
        }

        public string InitEpisode(EpisodeModel episode)
        {
            return StoreEpisodeInDatabase(episode);
        }

        public void StartEpisode(string id, IFormFile file)
        {
            var episode = GetEpisodeFromDatabase(id);
            if (episode == null) throw new System.ArgumentException("wrong id");

            StartEpisode(episode);
        }

        public void StartEpisode(EpisodeModel uploadedEpisode)
        {
            
            SendEpisodeToAuphonic();
            CreatePullRequest();
        }

        private string StoreEpisodeInDatabase(EpisodeModel episode)
        {
            try
            {
                return _database.StoreNewEpisode(episode);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Database error on {nameof(StoreEpisodeInDatabase)}");
                throw;
            }
        }

        private EpisodeModel GetEpisodeFromDatabase(string id)
        {
           return _database.RetrieveEpisode(id);
        }

        private void SendEpisodeToAuphonic()
        {

        }

        private void CreatePullRequest()
        {

        }
    }
}
