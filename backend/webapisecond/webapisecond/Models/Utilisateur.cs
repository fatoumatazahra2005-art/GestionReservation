using System.ComponentModel.DataAnnotations;

namespace webapisecond.Models;

public class Utilisateur
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string MotDePasseHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Enseignant";

    public record LoginDto([Required] string Email, [Required] string MotDePasse);
    public record TokenResponseDto(string Token, DateTime Expiration, string Email, string Role);

}