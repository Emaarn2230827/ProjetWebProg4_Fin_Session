namespace BoutiqueShoes.Models
{
    public class DataTranfer
    {
        public string ShoesName { get; set; }
        public double ShoesPrice { get; set; }
        public bool Disponibilite { get; set; }
        public string ShoesDescription { get; set; }
        public string LienPaiement { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
