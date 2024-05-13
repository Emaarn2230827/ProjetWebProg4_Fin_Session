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


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommandeShoes(int id, [FromBody] CommandeShoes commandeShoesUpdate)
        {
            try
            {
                var user = GetUserName();

                var shoesCommande = await _context.CommandeShoes.FirstOrDefaultAsync(cs => cs.CommandeShoesId == id);

                if (shoesCommande == null)
                {
                    return NotFound("CommandeShoes not found.");
                }

                var commande = await _context.Commande.FirstOrDefaultAsync(c => c.CommandeId == commandeShoesUpdate.CommandeId && c.ProprietaireCommande == user);

                if (commande == null)
                {
                    return NotFound("Commande not found.");
                }
                // Mettre à jour les attributs de la commande de chaussures
                shoesCommande.ShoesId = commandeShoesUpdate.ShoesId;
                shoesCommande.QuantiteCommande = commandeShoesUpdate.QuantiteCommande;

                _context.Entry(shoesCommande).State = EntityState.Modified;

                await _context.SaveChangesAsync();


                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandeShoesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors du traitement de la requête : {ex.Message}");
            }
        }



        [HttpPost]
        public async Task<ActionResult<CommandeShoes>> PostCommandeShoes([FromBody] CommandeShoes panier)
        {
            try
            {
                var userName = GetUserName();

                if (userName == null)
                {
                    return Unauthorized();
                }

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var nouvelleCommande = new Commande
                        {
                            DateCommande = DateTime.Now,
                            ProprietaireCommande = userName
                        };

                        _context.Commande.Add(nouvelleCommande);

                        await _context.SaveChangesAsync();

                        var commandeShoes = new CommandeShoes
                        {
                            CommandeId = nouvelleCommande.CommandeId,
                            ShoesId = panier.ShoesId,
                            QuantiteCommande = panier.QuantiteCommande
                        };

                        var shoes = await _context.Shoes.FirstOrDefaultAsync(s => s.ShoesId == panier.ShoesId);

                        if (shoes != null)
                        {
                            for (int i = 0; i < shoes.ShoesSize.Length; i++)
                            {
                                if (shoes.ShoesSize[i] == panier.TailleShoes)
                                {
                                    if (shoes.TotalParTaille[i] >= panier.QuantiteCommande)
                                    {
                                        commandeShoes.TailleShoes = shoes.ShoesSize[i];
                                       // int resteEnStock = shoes.NbrEnStock - panier.QuantiteCommande;
                                        _context.CommandeShoes.Add(commandeShoes);
                                        await _context.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        return NotFound("Out of stock");
                                    }
                                }
                            }
                        }
                        else
                        {
                            return NotFound("Shoes doesn't exist");
                        }

                        await transaction.CommitAsync();

                        return CreatedAtAction("GetCommandeShoes", new { id = commandeShoes.CommandeShoesId }, commandeShoes);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return StatusCode(500, $"Une erreur s'est produite lors du traitement de la requête : {ex.Message}");
                    }
                }
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
