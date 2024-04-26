namespace BoutiqueShoes.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public DateTime? DateCommande { get; set; }
        //ajouter id user apres avoir fais identity
        //ajouter l'etat de la commande
    }
}
