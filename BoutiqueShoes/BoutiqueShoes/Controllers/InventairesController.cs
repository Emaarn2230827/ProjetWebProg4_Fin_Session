using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoutiqueShoes.Data;
using BoutiqueShoes.Models;

namespace BoutiqueShoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventairesController : ControllerBase
    {
        private readonly BoutiqueShoesContext _context;

        public InventairesController(BoutiqueShoesContext context)
        {
            _context = context;
        }

        // GET: api/Inventaires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventaire>>> GetInventaire()
        {
          if (_context.Inventaire == null)
          {
              return NotFound();
          }
            return await _context.Inventaire.ToListAsync();
        }

        // GET: api/Inventaires/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventaire>> GetInventaire(int id)
        {
          if (_context.Inventaire == null)
          {
              return NotFound();
          }
            var inventaire = await _context.Inventaire.FindAsync(id);

            if (inventaire == null)
            {
                return NotFound();
            }

            return inventaire;
        }

        // PUT: api/Inventaires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventaire(int id, Inventaire inventaire)
        {
            if (id != inventaire.InventaireId)
            {
                return BadRequest();
            }

            _context.Entry(inventaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventaireExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Inventaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventaire>> PostInventaire(Inventaire inventaire)
        {
          if (_context.Inventaire == null)
          {
              return Problem("Entity set 'BoutiqueShoesContext.Inventaire'  is null.");
          }
            _context.Inventaire.Add(inventaire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventaire", new { id = inventaire.InventaireId }, inventaire);
        }

        // DELETE: api/Inventaires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventaire(int id)
        {
            if (_context.Inventaire == null)
            {
                return NotFound();
            }
            var inventaire = await _context.Inventaire.FindAsync(id);
            if (inventaire == null)
            {
                return NotFound();
            }

            _context.Inventaire.Remove(inventaire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventaireExists(int id)
        {
            return (_context.Inventaire?.Any(e => e.InventaireId == id)).GetValueOrDefault();
        }
    }
}
