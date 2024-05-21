using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoutiqueShoes.Models
{
    public class CommandeShoes
    {
        public int CommandeShoesId { get; set; }
        [RegularExpression(@"^[0-9]+$")]
        public int CommandeId { get; set; }
        [RegularExpression(@"^[0-9]+$")]
        public int ShoesId { get; set; }
        [RegularExpression(@"^[0-9]+$")]
        public int QuantiteCommande { get; set; }

        [NotMapped]
        public string TailleShoes { get; set; }

        public Commande? Commande { get; set; }

      
    }
}
