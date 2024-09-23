using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class ReasonRefusedViewModel
    {
        public int id { get; set; }
        public string language { get; set; }
        public string description { get; set; }

    }
}
