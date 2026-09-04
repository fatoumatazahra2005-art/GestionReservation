using System.ComponentModel.DataAnnotations;

namespace webapisecond.DTOs
{
    public record CreateReservationDto(
     [Required] int SalleId,
     [Required] int EnseignantId,
     [Required] DateTime DateDebut,
     [Required] DateTime DateFin,
     [Required, StringLength(200)] string Motif
);
}
