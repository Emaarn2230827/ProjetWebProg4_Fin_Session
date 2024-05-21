
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoutiqueShoes.Models
{
    public class Shoes
    {
        public int ShoesId { get; set; }

        [Required(ErrorMessage = "Le nom de la chaussure est requis")]
        public string ShoesName { get; set; }

        [Required(ErrorMessage = "La photo de la chaussure est requise")]
        public byte[] Image { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Le prix de la chaussure est requis")]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix de la chaussure doit être positif")]
        public double ShoesPrice { get; set; }

        public bool? Disponible { get; set; }

        [Required(ErrorMessage = "Le lien de paiement de la chaussure est requis")]
        public string LienPaiement { get; set; }

        [Required(ErrorMessage = "La description de la chaussure est requise")]
        public string ShoesDescription { get; set; }

        [NotMapped]
        public int[] TotalParTaille { get; set; } = { 5, 5, 8, 8, 10, 10, 10 };
        [NotMapped]
        public string[] ShoesSize { get; set; } = { "38", "39", "40", "41", "42", "43", "44" };

        public int NbrEnStock = 56;
    }
}
