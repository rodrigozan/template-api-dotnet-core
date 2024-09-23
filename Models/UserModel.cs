
namespace api.Models
{
    public class FilterUserModel
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public string? username { get; set; }
        public int? profileId { get; set; }
        public int? carrierId { get; set; }
        public string? language { get; set; }
        public bool? active { get; set; }
    }

    public class NewUserModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int profileId { get; set; }
        public int? carrierId { get; set; }
        public List<string> branchs { get; set; } = new();
        public string language { get; set; }
        public bool loginAd { get; set; }
        public bool active { get; set; }
    }

    public class UpdateUserModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int profileId { get; set; }
        public int? carrierId { get; set; }
        public List<string> branchs { get; set; } = new();
        public string language { get; set; }
        public bool loginAd { get; set; }
        public bool active { get; set; }
    }

}
