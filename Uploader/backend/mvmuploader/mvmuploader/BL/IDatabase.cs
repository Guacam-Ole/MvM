using MvM.Uploader.Backend.Web.Models;
using MvM.Uploader.Backend.Web.Models.Db;

namespace MvM.Uploader.Backend.Web.BL
{
    public interface IDatabase
    {
        void ChangeUploadCode(string hashedCode);
        string ReadUploadCodeHash();
        LiteEpisodeModel RetrieveEpisode(string id);
        string StoreNewEpisode(EpisodeModel uploadmodel);
    }
}