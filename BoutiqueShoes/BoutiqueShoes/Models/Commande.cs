using System.ComponentModel.DataAnnotations;

namespace BoutiqueShoes.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public DateTime? DateCommande { get; set; }
        //Id de l'utilisateur
        public int Id { get; set; }
        //ajouter l'adresse de livraison
        public int? NumeroRue { get; set; }
        public string? NomRue { get; set; }

        //[StringLength(7, MinimumLength = 6)]
        //[RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Code postal invalide")]
        public string? CodePostal { get; set; }


        
    }
}
