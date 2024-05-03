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
            return await _context.CommandeShoes.ToListAsync();
        }

        // GET: api/CommandeShoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommandeShoes>> GetCommandeShoes(int id)
        {
          if (_context.CommandeShoes == null)
          {
              return NotFound();
          }
            var commandeShoes = await _context.CommandeShoes.FindAsync(id);

            if (commandeShoes == null)
            {
                return NotFound();
            }

            return commandeShoes;
        }

        // PUT: api/CommandeShoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommandeShoes(int id, CommandeShoes commandeShoes)
        {
            if (id != commandeShoes.CommandeShoesId)
            {
                return BadRequest();
            }

            _context.Entry(commandeShoes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/CommandeShoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommandeShoes>> PostCommandeShoes(CommandeShoes commandeShoes)
        {
          if (_context.CommandeShoes == null)
          {
              return Problem("Entity set 'BoutiqueShoesContext.CommandeShoes'  is null.");
          }
            _context.CommandeShoes.Add(commandeShoes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommandeShoes", new { id = commandeShoes.CommandeShoesId }, commandeShoes);
        }

        // DELETE: api/CommandeShoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommandeShoes(int id)
        {
            if (_context.CommandeShoes == null)
            {
                return NotFound();
            }
            var commandeShoes = await _context.CommandeShoes.FindAsync(id);
            if (commandeShoes == null)
            {
                return NotFound();
            }

            _context.CommandeShoes.Remove(commandeShoes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommandeShoesExists(int id)
        {
            return (_context.CommandeShoes?.Any(e => e.CommandeShoesId == id)).GetValueOrDefault();
        }
    }
}
