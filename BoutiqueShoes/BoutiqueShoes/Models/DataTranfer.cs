namespace BoutiqueShoes.Models
{
    public class DataTranfer
    {
        public string ShoesName { get; set; }
        public double ShoesPrice { get; set; }
        public bool Disponible { get; set; }
        public string ShoesDescription { get; set; }
        public string LienPaiement { get; set; }
        public IFormFile Image { get; set; }
    }
}
