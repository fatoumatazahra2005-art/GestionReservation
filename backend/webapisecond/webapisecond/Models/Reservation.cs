namespace webapisecond.Models;

public class Reservation
{
    public int Id { get; set; }

    public int SalleId { get; set; }
    public Salle? Salle { get; set; }

    public int EnseignantId { get; set; }
    public Enseignant? Enseignant { get; set; }

    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public string Motif { get; set; } = string.Empty;
    public StatutReservation Statut { get; set; } = StatutReservation.Confirmee;
}