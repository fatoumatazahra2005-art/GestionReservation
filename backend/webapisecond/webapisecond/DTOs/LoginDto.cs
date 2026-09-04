using System.ComponentModel.DataAnnotations;

namespace webapisecond.DTOs
{
    public record LoginDto(
        [Required] string Email,
        [Required] string MotDePasse
    );
}