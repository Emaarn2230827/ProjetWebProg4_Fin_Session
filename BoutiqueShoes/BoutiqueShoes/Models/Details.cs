using System.ComponentModel.DataAnnotations;

namespace BoutiqueShoes.Models
{
    public class Details
    {
        public int DetailsId { get; set; }

        //[Required(ErrorMessage = "La taille de la chaussure est requise")]
        //[Range(5, 12, ErrorMessage = "La taille de la chaussure doit être comprise entre 0 et 100")]
        public int TailleChaussure { get; set; }

        //[Required(ErrorMessage = "La marque de la chaussure est requise")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "La marque de la chaussure ne doit pas contenir de nombre")]
        public string MarqueChaussure { get; set; }

        //[Required(ErrorMessage = "La couleur de la chaussure est requise")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "La couleur de la chaussure ne doit pas contenir de nombre")]
        public string CouleurChaussure { get; set; }

        //[Required(ErrorMessage = "La description de la chaussure est requise")]
        public string DescriptionChaussure { get; set; }
    }
}
