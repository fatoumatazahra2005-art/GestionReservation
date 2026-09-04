namespace webapisecond.Models;

public class Enseignant
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Departement { get; set; } = string.Empty;
}