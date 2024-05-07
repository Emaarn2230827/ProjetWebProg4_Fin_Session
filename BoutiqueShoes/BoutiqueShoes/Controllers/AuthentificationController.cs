using BoutiqueShoes.Authentification;
using BoutiqueShoes.Data;
using BoutiqueShoes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BoutiqueShoes.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly BoutiqueShoesContext _context;

        public AuthentificationController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            BoutiqueShoesContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var RolesUtilisateur = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new (ClaimTypes.Name, user.UserName),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in RolesUtilisateur)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            // Créer les informations personnelles associées à l'utilisateur
            var personalInfo = new PersonnalInformation
            {
                UserId = user.Id,
                NumeroRue = model.NumeroRue,
                NomRue = model.NomRue,
                Ville = model.Ville,
                CodePostal = model.CodePostal
            };
            _context.PersonnalInformation.Add(personalInfo);
            await _context.SaveChangesAsync();


            if (!await _roleManager.RoleExistsAsync(RolesUtilisateurs.Utilisateur))
                await _roleManager.CreateAsync(new IdentityRole(RolesUtilisateurs.Utilisateur));

            if (await _roleManager.RoleExistsAsync(RolesUtilisateurs.Utilisateur))
            {
                await _userManager.AddToRoleAsync(user, RolesUtilisateurs.Utilisateur);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(RolesUtilisateurs.Administrateur))
                await _roleManager.CreateAsync(new IdentityRole(RolesUtilisateurs.Administrateur));
            if (!await _roleManager.RoleExistsAsync(RolesUtilisateurs.Utilisateur))
                await _roleManager.CreateAsync(new IdentityRole(RolesUtilisateurs.Utilisateur));

            if (await _roleManager.RoleExistsAsync(RolesUtilisateurs.Administrateur))
            {
                await _userManager.AddToRoleAsync(user, RolesUtilisateurs.Administrateur);
            }
            if (await _roleManager.RoleExistsAsync(RolesUtilisateurs.Utilisateur))
            {
                await _userManager.AddToRoleAsync(user, RolesUtilisateurs.Utilisateur);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
