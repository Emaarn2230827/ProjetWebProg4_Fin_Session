
using System.ComponentModel.DataAnnotations;

namespace BoutiqueShoes.Models
{
    public class Shoes
    {
        public int ShoesId { get; set; }

        //[Required(ErrorMessage = "Le nom de la chaussure est requis")]
        public string ShoesName { get; set; }

        //[Required(ErrorMessage = "La photo de la chaussure est requise")]
        public byte[]? Image { get; set; }

        //[Required(ErrorMessage = "Le prix de la chaussure est requis")]
        //[Range(0, double.MaxValue, ErrorMessage = "Le prix de la chaussure doit être positif")]
        //[DataType(DataType.Currency)]
        public double ShoesPrice { get; set; }
        public bool? Disponible { get; set; }
        public int ShoesSize { get; set; }
        public string ShoesDescription { get; set; }
        public int NbrEnStock { get; set; }
    }
}
