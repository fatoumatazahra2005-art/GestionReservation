using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapisecond.Data;
using webapisecond.DTOs;
using webapisecond.Services;

namespace webapisecond.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginDto dto)
        {
            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            Console.WriteLine($"[Login] Email={dto.Email} UtilisateurTrouve={utilisateur is not null}");

            if (utilisateur is null)
            {
                return Unauthorized("Email ou mot de passe incorrect.");
            }

            bool passwordOk = BCrypt.Net.BCrypt.Verify(dto.MotDePasse, utilisateur.MotDePasseHash);
            if (!passwordOk)
            {
                return Unauthorized("Email ou mot de passe incorrect.");
            }

            var token = _tokenService.GenererToken(utilisateur);
            return Ok(new TokenResponseDto(
                token,
                DateTime.UtcNow.AddMinutes(60),
                utilisateur.Email,
                utilisateur.Role
            ));
        }
    }
}