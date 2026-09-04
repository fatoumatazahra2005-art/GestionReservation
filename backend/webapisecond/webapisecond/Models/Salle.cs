namespace webapisecond.Models;

public class Salle
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public int Capacite { get; set; }
    public string Batiment { get; set; } = string.Empty;
    public string? Equipements { get; set; }

    public List<Reservation> Reservations { get; set; } = new();
}