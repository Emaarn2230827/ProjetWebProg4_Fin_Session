namespace BoutiqueShoes.Models
{
    public class PersonnalInformation
    {
        public int PersonnalInformationId { get; set; }
        public string? UserId { get; set; }
        //ajouter l'adresse de livraison
        public int? NumeroRue { get; set; }
        public string? NomRue { get; set; }
        public string? Ville { get; set; }

        //[StringLength(7, MinimumLength = 6)]
        //[RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Code postal invalide")]
        public string? CodePostal { get; set; }
    }
}
