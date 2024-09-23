using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class UserViewModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string? password { get; set; }
        public int profileId { get; set; }
        public string profile { get; set; }
        public string language { get; set; }
        public bool loginAd { get; set; }
        public bool active { get; set; }
    }
}
