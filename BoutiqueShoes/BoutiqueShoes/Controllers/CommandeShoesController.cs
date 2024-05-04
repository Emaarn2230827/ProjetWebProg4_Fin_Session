using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoutiqueShoes.Data;
using BoutiqueShoes.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BoutiqueShoes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeShoesController : ControllerBase
    {
        private readonly BoutiqueShoesContext _context;

        public CommandeShoesController(BoutiqueShoesContext context)
        {
            _context = context;
        }

        // GET: api/CommandeShoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandeShoes>>> GetCommandeShoes()
        {
            if (_context.CommandeShoes == null)
            {
                return NotFound();
            }

            var user = GetUserName();

            if (user == null)
            {
                return Unauthorized();
            }
            return await _context.CommandeShoes
            .Include(c => c.Commande)
           .Where(p => p.Commande.ProprietaireCommande == user)
           .ToListAsync();
        }

        // GET: api/CommandeShoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommandeShoes>> GetCommandeShoes(int id)
        {
            if (_context.CommandeShoes == null)
            {
                return NotFound();
            }
            var user = GetUserName();

            var commandeShoes = await _context.CommandeShoes
             .Include(cs => cs.Commande)
             .Where(cs => cs.Commande.ProprietaireCommande == user && cs.CommandeShoesId == id)
             .FirstOrDefaultAsync();

            return commandeShoes;
        }

        // PUT: api/CommandeShoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCommandeShoes(int id, [Bind(nameof(CommandeShoes.ShoesId), nameof(CommandeShoes.QuantiteCommande))] CommandeShoes commandeShoes)
        //{
        //    try
        //    {
        //        if (id != commandeShoes.CommandeShoesId)
        //        {
        //            return BadRequest();
        //        }

        //        var shoesCommande = await _context.CommandeShoes.FindAsync(id);

        //        if (shoesCommande != null && shoesCommande.Commande.ProprietaireCommande == GetUserName())
        //        {
        //            shoesCommande.QuantiteCommande = commandeShoes.QuantiteCommande;

        //            var shoes = await _context.Shoes.FindAsync(commandeShoes.ShoesId);

        //            if (shoes != null)
        //            {
        //                shoesCommande.ShoesId = commandeShoes.ShoesId;

        //                // Récupérer la commande associée
        //                var commande = await _context.Commande.FirstOrDefaultAsync(c => c.CommandeId == commandeShoes.Commande.CommandeId);

        //                // Assigner la commande associée à la commande de chaussures
        //                shoesCommande.Commande = commande;

        //                _context.Entry(shoesCommande).State = EntityState.Modified;
        //            }
        //        }

        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CommandeShoesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Une erreur s'est produite lors du traitement de la requête : {ex.Message}");
        //    }
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCommandeShoes(int id, [Bind(nameof(CommandeShoes.ShoesId),nameof(CommandeShoes.CommandeShoesId), nameof(CommandeShoes.QuantiteCommande), nameof(CommandeShoes.CommandeId))] CommandeShoes commandeShoesUpdate, [FromBody]int commandeId)
        //{
        //    try
        //    {
        //        if (id != commandeShoesUpdate.CommandeShoesId)
        //        {
        //            return BadRequest();
        //        }

        //        var user = GetUserName();


        //        //recuperer la commande associée

        //        var commande = await _context.Commande.FirstOrDefaultAsync(c => c.ProprietaireCommande == user && c.CommandeId == commandeId);
        //        // Recherche de la commande de chaussures à mettre à jour

        //        if (commande != null)
        //        {
        //            var shoesCommande = await _context.CommandeShoes
        //           .FirstOrDefaultAsync(cs => cs.CommandeId == commande.CommandeId && cs.CommandeShoesId == id);

        //            if (shoesCommande == null)
        //            {
        //                return NotFound();
        //            }

        //            // Mettre à jour l'adresse de livraison dans la table Commande
        //            commande.NumeroRue = commandeShoesUpdate.Commande.NumeroRue;
        //            commande.NomRue = commandeShoesUpdate.Commande.NomRue;
        //            commande.Ville = commandeShoesUpdate.Commande.Ville;
        //            commande.CodePostal = commandeShoesUpdate.Commande.CodePostal;

        //            // Mettre à jour le ShoesId et la QuantiteCommande dans la table CommandeShoes
        //            shoesCommande.ShoesId = commandeShoesUpdate.ShoesId;
        //            shoesCommande.QuantiteCommande = commandeShoesUpdate.QuantiteCommande;

        //            _context.Entry(shoesCommande).State = EntityState.Modified;
        //            _context.Entry(commande).State = EntityState.Modified;

        //            // Enregistrer les modifications dans la base de données
        //            await _context.SaveChangesAsync();
        //            return NoContent();
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CommandeShoesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Une erreur s'est produite lors du traitement de la requête : {ex.Message}");
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCommandeShoes(int id, [FromBody] CommandeShoes commandeShoesUpdate)
        //{
        //    try
        //    {
        //        var user = GetUserName();
        //        // Récupérer la commande associée
        //        var commande = await _context.Commande.FirstOrDefaultAsync(c => c.CommandeId == commandeShoesUpdate.CommandeId && c.ProprietaireCommande == user);

        //        if (commande == null)
        //        {
        //            return NotFound("Commande not found.");
        //        }

        //        // Recherche de la commande de chaussures à mettre à jour
        //        var shoesCommande = await _context.CommandeShoes.FirstOrDefaultAsync(cs => cs.CommandeShoesId == id);

        //        if (shoesCommande == null)
        //        {
        //            return NotFound("CommandeShoes not found.");
        //        }

        //        // Mettre à jour les attributs de la commande
        //        commande.NumeroRue = commandeShoesUpdate.Commande.NumeroRue;
        //        commande.NomRue = commandeShoesUpdate.Commande.NomRue;
        //        commande.Ville = commandeShoesUpdate.Commande.Ville;
        //        commande.CodePostal = commandeShoesUpdate.Commande.CodePostal;

        //        // Mettre à jour les attributs de la commande de chaussures
        //        shoesCommande.ShoesId = commandeShoesUpdate.ShoesId;
        //        shoesCommande.QuantiteCommande = commandeShoesUpdate.QuantiteCommande;

        //        // Enregistrer les modifications dans la base de données
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Une erreur s'est produite lors du traitement de la requête : {ex.Message}");
        //    }
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommandeShoes(int id, [FromBody] CommandeShoes commandeShoesUpdate)
        {
            try
            {
                var user = GetUserName();

                // Recherche de la commande de chaussures à mettre à jour
                var shoesCommande = await _context.CommandeShoes.FirstOrDefaultAsync(cs => cs.CommandeShoesId == id);

                if (shoesCommande == null)
                {
                    return NotFound("CommandeShoes not found.");
                }

                // Mettre à jour les attributs de la commande de chaussures
                shoesCommande.ShoesId = commandeShoesUpdate.ShoesId;
                shoesCommande.QuantiteCommande = commandeShoesUpdate.QuantiteCommande;

                // Vérifiez d'abord si la commande associée existe
                if (commandeShoesUpdate.Commande.CommandeId != null)
                {
                    // Récupérer la commande associée
                    var commande = await _context.Commande.FirstOrDefaultAsync(c => c.CommandeId == commandeShoesUpdate.Commande.CommandeId && c.ProprietaireCommande == user);

                    if (commande == null)
                    {
                        return NotFound("Commande not found.");
                    }

                    // Mettre à jour les attributs de la commande
                    commande.NumeroRue = commandeShoesUpdate.Commande.NumeroRue;
                    commande.NomRue = commandeShoesUpdate.Commande.NomRue;
                    commande.Ville = commandeShoesUpdate.Commande.Ville;
                    commande.CodePostal = commandeShoesUpdate.Commande.CodePostal;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound("Command not found");
                }

                // Enregistrer les modifications dans la base de données
               

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors du traitement de la requête : {ex.Message}");
            }
        }




        [HttpPost]
        public async Task<ActionResult<CommandeShoes>> PostCommandeShoes([FromBody] ElementsPanier panier)
        {
            try
            {
                var userName = GetUserName();

                if (userName == null)
                {
                    return Unauthorized();
                }

                var nouvelleCommande = new Commande
                {
                    DateCommande = DateTime.Now,
                    ProprietaireCommande = userName,
                    NumeroRue = panier.NumeroRue,
                    NomRue = panier.NomRue,
                    Ville = panier.Ville,
                    CodePostal = panier.CodePostal,

                };

                _context.Commande.Add(nouvelleCommande);

                await _context.SaveChangesAsync();

                var commandeShoes = new CommandeShoes
                {
                    CommandeId = nouvelleCommande.CommandeId,
                    ShoesId = panier.IdShoes,
                    QuantiteCommande = panier.Quantite
                };

                _context.CommandeShoes.Add(commandeShoes);

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCommandeShoes", new { id = commandeShoes.CommandeShoesId }, commandeShoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors du traitement de la requête : {ex.Message}");
            }
        }


        // DELETE: api/CommandeShoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommandeShoes(int id)
        {
            if (_context.CommandeShoes == null)
            {
                return NotFound();
            }
            var user = GetUserName();

            var commandeShoes = await _context.CommandeShoes
             .Include(cs => cs.Commande)
             .Where(cs => cs.Commande.ProprietaireCommande == user && cs.CommandeShoesId == id)
             .FirstOrDefaultAsync();



            if (commandeShoes == null)
            {
                return NotFound();
            }

            _context.CommandeShoes.Remove(commandeShoes);
            _context.Commande.Remove(commandeShoes.Commande);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommandeShoesExists(int id)
        {
            return (_context.CommandeShoes?.Any(e => e.CommandeShoesId == id)).GetValueOrDefault();
        }

        private string? GetUserName()
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == ClaimTypes.Name))
                return currentUser.Claims.FirstOrDefault(c => c.Type ==
               ClaimTypes.Name)?.Value;
            return null;
        }
    }
}
