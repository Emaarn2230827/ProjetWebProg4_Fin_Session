﻿using System.ComponentModel.DataAnnotations;

namespace BoutiqueShoes.Authentification
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Le username est requis")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        public string? Password { get; set; }


        [EmailAddress]
        [Required(ErrorMessage = "l'adresse courriel est requis")]
        public string? Email { get; set; }

        //ajouter l'adresse de l'utilisateur


    }
}
