using System.ComponentModel.DataAnnotations;

namespace BoutiqueShoes.Models
{
    public class Inventaire
    {
        public int InventaireId { get; set; }
        public int NombreEnStocke { get; set; }
    }
}
