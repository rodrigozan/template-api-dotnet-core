﻿using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class NewLoginModel
    {
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe o perfil")]
        public int ProfileId { get; set; }

        public string Language { get; set; } = "PT";

        public bool Active { get; set; } = true;

    }
}