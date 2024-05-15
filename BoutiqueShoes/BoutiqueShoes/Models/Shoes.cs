
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoutiqueShoes.Models
{
    public class Shoes
    {
        public int ShoesId { get; set; }

        //[Required(ErrorMessage = "Le nom de la chaussure est requis")]
        public string ShoesName { get; set; }

        //[Required(ErrorMessage = "La photo de la chaussure est requise")]
      
        public byte[] Image { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; }

        //[Required(ErrorMessage = "Le prix de la chaussure est requis")]
        //[Range(0, double.MaxValue, ErrorMessage = "Le prix de la chaussure doit être positif")]
        //[DataType(DataType.Currency)]

        public double ShoesPrice { get; set; }

        public bool? Disponible { get; set; }

        public string LienPaiement { get; set; }

        public string ShoesDescription { get; set; }

        [NotMapped]
        public int[] TotalParTaille { get; set; } = { 5, 5, 8, 8, 10, 10, 10 };
        [NotMapped]
        public string[] ShoesSize { get; set; } = { "38", "39", "40", "41", "42", "43", "44" };

        public int NbrEnStock = 56;

        //public List<Commande> Commandes { get; set;}
    }
}
