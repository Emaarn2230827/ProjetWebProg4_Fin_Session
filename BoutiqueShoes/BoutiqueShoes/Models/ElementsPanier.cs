namespace BoutiqueShoes.Models
{
    public class ElementsPanier
    {
        public int IdShoes { get; set; }
        public int Quantite { get; set; }
        public int NumeroRue { get; set; }
        public string NomRue { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }

        public double Montant(Shoes shoes)
        {
            return Quantite * shoes.ShoesPrice;
        }
    }
}
