using System.ComponentModel.DataAnnotations;

namespace webapisecond.DTOs
{
    public record ReservationDto(
        int Id,
        string SalleNom,
        string EnseignantNomComplet,
       DateTime DateDebut,
       DateTime DateFin, 
       string Motif, 
       string Statut);



}
