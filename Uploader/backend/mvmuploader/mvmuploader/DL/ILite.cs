using LiteDB;

namespace MvM.Uploader.Backend.Web.DL
{
    public interface ILite
    {
        T Select<T>(string collectionName, BsonValue id);
        void UpSert<T>(string collectionName, T contents);
    }
}