using BoutiqueShoes.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BoutiqueShoes.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = new BoutiqueShoesContext(
                serviceProvider.GetRequiredService<DbContextOptions<BoutiqueShoesContext>>());
            SeedShoes(context);
        }

        public static void SeedShoes(BoutiqueShoesContext context)
        {
            //vefrifie si la table est vide


            if (context.Shoes.Count() > 0)
                return;

            context.Shoes.AddRange(new Shoes { ShoesName = "Vans", ShoesPrice = 100 ,ShoesSize = 39, ShoesDescription ="La chaussure", NbrEnStock = 5},
                new Shoes {ShoesName = "Adidas", ShoesPrice = 150, ShoesSize = 40, ShoesDescription = "La chaussure", NbrEnStock = 8 },
                new Shoes {ShoesName = "Puma", ShoesPrice = 120, ShoesSize = 42, ShoesDescription = "La chaussure", NbrEnStock = 15 },
                new Shoes {ShoesName = "Reebok", ShoesPrice = 80, ShoesSize = 37, ShoesDescription = "La chaussure", NbrEnStock = 10 });

            context.SaveChanges();
        }


        //public static void SeedCommande(BoutiqueShoesContext context)
        //{
        //    if (context.Commande.Count() > 0)
        //        return;

        //    context.Commande.Add(new Commande { DateCommande = DateTime.Now });
        //    context.SaveChanges();
        //}

        //public static void SeedCommandeShoes(BoutiqueShoesContext context)
        //{
        //    if (context.CommandeShoes.Count() > 0)
        //        return;

        //    context.CommandeShoes.Add(new CommandeShoes { CommandeId = 1, ShoesId = 11, QuantiteCommande = 1 });
        //    context.SaveChanges();
        //}

        //private string? GetUserName()
        //{
        //    var currentUser = HttpContext.User;
        //    if (currentUser.HasClaim(c => c.Type == ClaimTypes.Name))
        //        return currentUser.Claims.FirstOrDefault(c => c.Type ==
        //       ClaimTypes.Name)?.Value;
        //    return null;
        //}

    }
}
