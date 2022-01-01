namespace MvM.Uploader.Backend.Web.Models
{
    public class Secrets
    {
        public string Salt { get; set; }    
        public string Admin { get; set; }   
        public SecretsAuthenticationPair Auphonic { get; set; } 
        public SecretsAuthenticationPair Github { get; set; }   
    }

    public class SecretsAuthenticationPair
    {
        public string Username { get; set; }
        public string Password { get; set; }    

    }
}
