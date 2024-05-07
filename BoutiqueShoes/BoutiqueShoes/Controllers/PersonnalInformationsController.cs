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
    public class PersonnalInformationsController : ControllerBase
    {
        private readonly BoutiqueShoesContext _context;

        public PersonnalInformationsController(BoutiqueShoesContext context)
        {
            _context = context;
        }

        // GET: api/PersonnalInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonnalInformation>>> GetPersonnalInformation()
        {
          if (_context.PersonnalInformation == null)
          {
              return NotFound();
          }
            return await _context.PersonnalInformation.ToListAsync();
        }

        // GET: api/PersonnalInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonnalInformation>> GetPersonnalInformation(int id)
        {
          if (_context.PersonnalInformation == null)
          {
              return NotFound();
          }
            var personnalInformation = await _context.PersonnalInformation.FindAsync(id);

            if (personnalInformation == null)
            {
                return NotFound();
            }

            return personnalInformation;
        }

        // PUT: api/PersonnalInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonnalInformation(int id, PersonnalInformation personnalInformation)
        {
            if (id != personnalInformation.PersonnalInformationId)
            {
                return BadRequest();
            }

            _context.Entry(personnalInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnalInformationExists(id))
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

        // POST: api/PersonnalInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonnalInformation>> PostPersonnalInformation(PersonnalInformation personnalInformation)
        {
          if (_context.PersonnalInformation == null)
          {
              return Problem("Entity set 'BoutiqueShoesContext.PersonnalInformation'  is null.");
          }
            _context.PersonnalInformation.Add(personnalInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonnalInformation", new { id = personnalInformation.PersonnalInformationId }, personnalInformation);
        }

        // DELETE: api/PersonnalInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonnalInformation(int id)
        {
            if (_context.PersonnalInformation == null)
            {
                return NotFound();
            }
            var personnalInformation = await _context.PersonnalInformation.FindAsync(id);
            if (personnalInformation == null)
            {
                return NotFound();
            }

            _context.PersonnalInformation.Remove(personnalInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonnalInformationExists(int id)
        {
            return (_context.PersonnalInformation?.Any(e => e.PersonnalInformationId == id)).GetValueOrDefault();
        }
    }
}
