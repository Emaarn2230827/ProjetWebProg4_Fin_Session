using System.ComponentModel.DataAnnotations;

namespace BoutiqueShoes.Models
{
    public class CommandeShoes
    {
        public int CommandeShoesId { get; set; }
        public int CommandeId { get; set; }
        public int ShoesId { get; set; }
        // [RegularExpression(@"^[0-9]+$")]
        public int QuantiteCommande { get; set; }

        public Commande? Commande { get; set; }

      
    }
}
