using LiteDB;

namespace MvM.Uploader.Backend.Web.DL
{
    public class Lite : ILite
    {
        private const string DatabasePath = "Properties/mvm.db";

        public void UpSert<T>(string collectionName, T contents)
        {
            using var db = new LiteDatabase(DatabasePath);
            var collection = db.GetCollection<T>(collectionName);
            collection.Upsert(contents);
        }

        public T Select<T>(string collectionName, BsonValue id)
        {
            using var db = new LiteDatabase(DatabasePath);
            var codecollection = db.GetCollection<T>(collectionName);
            return codecollection.FindById(id);
        }

    }
}