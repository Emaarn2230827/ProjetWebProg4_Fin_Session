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

            string path = "Images/nike.png";
            context.Shoes.AddRange(new Shoes { ShoesName = "Vans", ShoesPrice = 100, ShoesDescription = "La chaussure", Disponible = true, Image = LoadImage(path) },
                new Shoes { ShoesName = "Adidas", ShoesPrice = 150, ShoesDescription = "La chaussure", Disponible = true, Image = LoadImage(path) },
                new Shoes { ShoesName = "Puma", ShoesPrice = 120, ShoesDescription = "La chaussure", Disponible = true, Image = LoadImage(path) },
                new Shoes { ShoesName = "Reebok", ShoesPrice = 80, ShoesDescription = "La chaussure", Disponible = true, Image = LoadImage(path) });

            context.SaveChanges();
        }

        private static byte[] LoadImage(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                return buffer;
            }
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
