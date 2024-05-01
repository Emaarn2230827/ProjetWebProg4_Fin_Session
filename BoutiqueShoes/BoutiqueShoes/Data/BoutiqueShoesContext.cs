using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoutiqueShoes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueShoes.Data
{
    public class BoutiqueShoesContext : IdentityDbContext<IdentityUser>
    {
        public BoutiqueShoesContext (DbContextOptions<BoutiqueShoesContext> options)
            : base(options)
        {
        }
        public DbSet<BoutiqueShoes.Models.Shoes>? Shoes { get; set; }
        public DbSet<BoutiqueShoes.Models.CommandeShoes>? CommandeShoes { get; set; }
        public DbSet<BoutiqueShoes.Models.Commande>? Commande { get; set; }

    }
}
