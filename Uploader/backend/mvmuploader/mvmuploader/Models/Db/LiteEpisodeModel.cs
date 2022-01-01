using System;

namespace MvM.Uploader.Backend.Web.Models.Db
{
    public class LiteEpisodeModel : EpisodeModel
    {
        public string Id { get; set; } 
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Converted { get; set; }
        public DateTime? Accepted { get; set; }
        public DateTime? Rejected { get; set; }

        public LiteEpisodeModel() { 
        
        }
        public LiteEpisodeModel(EpisodeModel basemodel)
        {
            Id = DateTime.Now.ToString("yyyyMMddHHmmss-")+Guid.NewGuid().ToString();
            foreach (var property in typeof(EpisodeModel).GetProperties())
            {
                if (property.GetCustomAttributes(typeof(StoreInDatabaseAttribute), false) == null)
                {
                    continue;
                }
                typeof(LiteEpisodeModel).GetProperty(property.Name).SetValue(this, property.GetValue(basemodel), null);
            }
        }
    }
}
