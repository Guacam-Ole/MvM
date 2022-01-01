using MvM.Uploader.Backend.Web.DL;
using MvM.Uploader.Backend.Web.Models;
using MvM.Uploader.Backend.Web.Models.Db;

namespace MvM.Uploader.Backend.Web.BL
{
    public class Database : IDatabase
    {
        private readonly Lite _lite;
        private const string _collectionCode = "code";
        private const string _collectionEpisodes = "episodes";

        public Database(Lite lite)
        {
            _lite = lite;
        }

        public void ChangeUploadCode(string hashedCode)
        {
            _lite.UpSert(_collectionCode, new LiteSettingsModel { Code = hashedCode });
        }

        public string ReadUploadCodeHash()
        {
            return _lite.Select<LiteSettingsModel>(_collectionCode, 1).Code;
        }

        public string StoreNewEpisode(EpisodeModel uploadmodel)
        {
            var episodeModel = new LiteEpisodeModel(uploadmodel);
            _lite.UpSert(_collectionEpisodes, episodeModel);
            return episodeModel.Id;
        }

        public LiteEpisodeModel RetrieveEpisode(string id)
        {
            return _lite.Select<LiteEpisodeModel>(_collectionEpisodes, id);
        }
    }
}