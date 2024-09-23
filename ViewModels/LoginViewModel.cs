﻿using System.ComponentModel.DataAnnotations;
using System.Data;

namespace api.ViewModels
{
    public class LoginViewModel
    {
        public bool authenticated { get; set; } = true;
        public string message { get; set; } = "";
        public string name { get; set; }
        public string email { get; set; }
        public string profile  { get; set; }
        public string language  { get; set; }
        public int expireToken { get; set; } = 43200000;
        public int timeAway { get; set; } = 900000;
        public string token { get; set; }
    }
}