using System.ComponentModel.DataAnnotations;

namespace BoutiqueShoes.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public DateTime? DateCommande { get; set; }
        //Id de l'utilisateur
        public string ProprietaireCommande { get; set; }
        
    }
}
