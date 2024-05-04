using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoutiqueShoes.Data;
using BoutiqueShoes.Models;
using BoutiqueShoes.Authentification;
using System.Security.Claims;

namespace BoutiqueShoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {
        private readonly BoutiqueShoesContext _context;

        public ShoesController(BoutiqueShoesContext context)
        {
            _context = context;
        }

        // GET: api/Shoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoes>>> GetShoes()
        {
          if (_context.Shoes == null)
          {
              return NotFound();
          }

            return await _context.Shoes.ToListAsync();
        }

        // GET: api/Shoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shoes>> GetShoes(int id)
        {
          if (_context.Shoes == null)
          {
              return NotFound();
          }
            var shoes = await _context.Shoes.FindAsync(id);

            if (shoes == null)
            {
                return NotFound();
            }

            return shoes;
        }

        // PUT: api/Shoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoes(int id,
            [Bind(nameof(Shoes.ShoesId), nameof(Shoes.NbrEnStock), nameof(Shoes.ShoesSize), nameof(Shoes.ShoesPrice), nameof(Shoes.ShoesDescription), nameof(Shoes.ShoesName))] Shoes shoes)
        {
            if (id != shoes.ShoesId)
            {
                return BadRequest();
            }

            var chaussureDB = await _context.Shoes.FindAsync(id);

            if (chaussureDB != null && IsAdmin())
            {
                chaussureDB.NbrEnStock = shoes.NbrEnStock;
                chaussureDB.ShoesPrice = shoes.ShoesPrice;
                chaussureDB.ShoesSize = shoes.ShoesSize;
                chaussureDB.ShoesDescription = shoes.ShoesDescription;
                chaussureDB.ShoesName = shoes.ShoesName;

                _context.Entry(chaussureDB).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoesExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
           

            return NoContent();
        }

        // POST: api/Shoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shoes>> PostShoes(Shoes shoes)
        {
          if (_context.Shoes == null)
          {
              return Problem("Entity set 'BoutiqueShoesContext.Shoes'  is null.");
          }
            _context.Shoes.Add(shoes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoes", new { id = shoes.ShoesId }, shoes);
        }

        // DELETE: api/Shoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoes(int id)
        {
            if (_context.Shoes == null)
            {
                return NotFound();
            }
            var shoes = await _context.Shoes.FindAsync(id);
            if (shoes == null)
            {
                return NotFound();
            }

            _context.Shoes.Remove(shoes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoesExists(int id)
        {
            return (_context.Shoes?.Any(e => e.ShoesId == id)).GetValueOrDefault();
        }

        private bool IsAdmin()
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == ClaimTypes.Role))
                return RolesUtilisateurs.Administrateur == currentUser.Claims.First(c =>
               c.Type == ClaimTypes.Role).Value;
            return false;
        }

    }
}
