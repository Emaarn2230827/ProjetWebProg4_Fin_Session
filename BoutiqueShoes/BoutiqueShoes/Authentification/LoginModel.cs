using System.ComponentModel.DataAnnotations;

namespace BoutiqueShoes.Authentification
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Le nom utilisateur est obligatoire")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        public string? Password { get; set; }
    }
}
