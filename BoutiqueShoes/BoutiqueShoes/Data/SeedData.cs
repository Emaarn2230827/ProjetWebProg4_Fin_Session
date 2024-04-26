using BoutiqueShoes.Models;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueShoes.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = new BoutiqueShoesContext(
                serviceProvider.GetRequiredService<DbContextOptions<BoutiqueShoesContext>>());

            SeedDetails(context);
            SeedInventaires(context);
            SeedShoes(context);
            SeedCommande(context);
            SeedCommandeShoes(context);
        }

        public static void SeedShoes(BoutiqueShoesContext context)
        {
            //vefrifie si la table est vide


            if (context.Shoes.Count() > 0)
                return;

            context.Shoes.AddRange(new Shoes { ShoesName = "Vans" , ShoesPrice = 100 ,InventaireId = 17,DetailsId = 1},
                new Shoes { ShoesName = "Adidas" , ShoesPrice = 150 ,InventaireId = 18,DetailsId = 2},
                new Shoes { ShoesName = "Puma" , ShoesPrice = 120 ,InventaireId = 19,DetailsId = 3},
                new Shoes { ShoesName = "Reebok" , ShoesPrice = 80 ,InventaireId = 20,DetailsId = 4});

            context.SaveChanges();
        }

        public static void SeedDetails(BoutiqueShoesContext context)
        {
            if (context.Details.Count() > 0)
                return;

            context.Details.AddRange(new Details { TailleChaussure = 6, CouleurChaussure = "Noir", MarqueChaussure = "Vans", DescriptionChaussure = "Meilleure chaussure"},
                new Details { TailleChaussure = 8, CouleurChaussure = "Blanc", MarqueChaussure = "Adidas", DescriptionChaussure = "Meilleure chaussure"},
                new Details { TailleChaussure = 10, CouleurChaussure = "Rouge", MarqueChaussure = "Puma", DescriptionChaussure = "Meilleure chaussure"},
                new Details { TailleChaussure = 12, CouleurChaussure = "Bleu", MarqueChaussure = "Reebok", DescriptionChaussure = "Meilleure chaussure"});

            context.SaveChanges();
        }

        public static void SeedInventaires(BoutiqueShoesContext context)
        {
            if (context.Inventaire.Count() > 0)
                return;

            context.Inventaire.AddRange(new Inventaire { NombreEnStocke = 30}, 
                new Inventaire { NombreEnStocke = 40}, 
                new Inventaire { NombreEnStocke = 50}, 
                new Inventaire { NombreEnStocke = 60});

            context.SaveChanges();
        }

        public static void SeedCommande(BoutiqueShoesContext context)
        {
            if (context.Commande.Count() > 0)
                return;

            context.Commande.Add(new Commande { DateCommande = DateTime.Now});
            context.SaveChanges();
        }

        public static void SeedCommandeShoes(BoutiqueShoesContext context)
        {
            if (context.CommandeShoes.Count() > 0)
                return;

            context.CommandeShoes.Add(new CommandeShoes { CommandeId = 1, ShoesId = 11,QuantiteCommande = 1});
            context.SaveChanges();
        }
        
    }
}
