//using BoutiqueShoes.Authentification;
//using BoutiqueShoes.Data;
//using BoutiqueShoes.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//namespace BoutiqueShoes.PannierController
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GestionPanierController : ControllerBase
//    {
//        private readonly BoutiqueShoesContext _context;

//        public GestionPanierController(BoutiqueShoesContext context)
//        {
//            _context = context;
//        }

//        [HttpPost]
//        [Route("ajouter-au-panier")]
//        public async Task<ActionResult> AjouterAuPanier([FromBody] int idProduit)
//        {
//            //if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
//            //{
//            //    return Unauthorized();
//            //}
//            int userId = 0;
//            if (IsUtilisateur()){
//                 userId = GetUserId().GetValueOrDefault();
//            }

           

//            if (userId == null)
//                return Unauthorized("Utilisateur non trouvé");

//            // Vérifier si le produit existe
//            var produit = await _context.Shoes.FindAsync(idProduit);
//            if (produit == null)
//            {
//                return NotFound("Produit non trouvé");
//            }

//            // Créer une nouvelle commande si l'utilisateur n'en a pas déjà une en cours
//            var commande = await _context.Commande.FirstOrDefaultAsync(c => c.Id == userId);
//            if (commande == null)
//            {
//                commande = new Commande { Id = userId, DateCommande = DateTime.Now };
//                _context.Commande.Add(commande);
//                await _context.SaveChangesAsync();
//            }

//            // Ajouter la chaussure à la commande
//            var commandeShoes = new CommandeShoes { CommandeId = commande.CommandeId, ShoesId = idProduit, QuantiteCommande = 1 };
//            _context.CommandeShoes.Add(commandeShoes);
//            await _context.SaveChangesAsync();

//            return Ok("Produit ajouté au panier");
//        }


//        [HttpDelete("{id}")]
//        public async Task<ActionResult> SupprimerElementDuPanier(int idProduit)
//        {
//            // Récupérer l'ID de l'utilisateur à partir du claim correspondant
//            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
//            if (userIdClaim == null)
//            {
//                return Unauthorized();
//            }

//            if (!int.TryParse(userIdClaim.Value, out int userId))
//            {
//                return Unauthorized();
//            }

//            // Rechercher le panier de l'utilisateur
//            var panier = await _context.CommandeShoes.FirstOrDefaultAsync(cs => cs.Commande.Id == userId && cs.ShoesId == idProduit);
//            if (panier == null)
//            {
//                return NotFound("Élément du panier non trouvé");
//            }

//            // Supprimer l'élément du panier
//            _context.CommandeShoes.Remove(panier);
//            await _context.SaveChangesAsync();

//            return Ok("Élément du panier supprimé avec succès");
//        }

//        //gerer la modification de panier

//        private bool IsUtilisateur()
//        {
//            var currentUser = HttpContext.User;
//            if (currentUser.HasClaim(c => c.Type == ClaimTypes.Role))
//                return RolesUtilisateurs.Utilisateur == currentUser.Claims.First(c =>
//               c.Type == ClaimTypes.Role).Value;
//            return false;
//        }

//        private int? GetUserId()
//        {
//            var currentUser = HttpContext.User;
//            if (currentUser.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
//                return int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type ==
//               ClaimTypes.NameIdentifier)?.Value);
//            return null;
//        }
//    }
//}
