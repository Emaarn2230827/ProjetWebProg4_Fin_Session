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
using Microsoft.AspNetCore.Authorization;

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

            var shoes = await _context.Shoes.ToListAsync();

            shoes.ForEach(shoe => shoe.ImageUrl = ConvertByteArrayToImageUrl(shoe.Image));
            return shoes;
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
            [Bind(nameof(Shoes.ShoesId), nameof(Shoes.Disponible), nameof(Shoes.ShoesPrice), nameof(Shoes.ShoesDescription), nameof(Shoes.ShoesName), nameof(Shoes.LienPaiement))] Shoes shoes)
        {
            if (id != shoes.ShoesId)
            {
                return BadRequest();
            }

            var chaussureDB = await _context.Shoes.FirstOrDefaultAsync(s => s.ShoesId == id);

            //if (chaussureDB != null && IsAdmin())
            //{
            //chaussureDB.NbrEnStock = shoes.NbrEnStock;
            shoes.Image = chaussureDB.Image; shoes.ImageUrl = chaussureDB.ImageUrl;
            chaussureDB.ImageUrl = chaussureDB.ImageUrl;
            chaussureDB.ShoesPrice = shoes.ShoesPrice;
            chaussureDB.Disponible = shoes.Disponible;
            chaussureDB.ShoesDescription = shoes.ShoesDescription;
            chaussureDB.ShoesName = shoes.ShoesName;
            chaussureDB.LienPaiement = shoes.LienPaiement;

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
            //}
            //else
            //{
            //    return Unauthorized();
            //}


            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Shoes>> PostShoes([FromForm] DataTranfer shoes)
        {
            if (_context.Shoes == null)
            {
                return Problem("Entity set 'BoutiqueShoesContext.Shoes'  is null.");
            }

            if (shoes.ImageFile == null || shoes.ImageFile.Length == 0)
            {
                return BadRequest("No image uploaded");
            }

            //if (IsAdmin())
            //{
            try
            {
                var imageBytes = await ReadFileAsync(shoes.ImageFile);

                var nouvelleChaussure = new Shoes
                {
                    ShoesName = shoes.ShoesName,
                    ShoesPrice = shoes.ShoesPrice,
                    ShoesDescription = shoes.ShoesDescription,
                    Disponible = true,
                    Image = imageBytes,
                    LienPaiement = shoes.LienPaiement
                };

                _context.Shoes.Add(nouvelleChaussure);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetShoes", new { id = nouvelleChaussure.ShoesId }, nouvelleChaussure);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        //}
        //else
        //{
        //    return Unauthorized();
        //}

    }

    private async Task<byte[]> ReadFileAsync(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    // DELETE: api/Shoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShoes(int id)
    {
        //if (IsAdmin())
        //{
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
        //}
        //else
        //{
        //    return Unauthorized();
        //}

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

    private string ConvertByteArrayToImageUrl(byte[] byteArray)
    {
        if (byteArray == null || byteArray.Length == 0)
        {
            return string.Empty;
        }

        string base64String = Convert.ToBase64String(byteArray);
        return $"data:image/png;base64,{base64String}";
    }

}
}
