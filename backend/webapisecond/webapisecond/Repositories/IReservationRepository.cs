using webapisecond.Models;

namespace webapisecond.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllAsync(
            int? salleId,
            DateTime? date);

        Task<Reservation?> GetByIdAsync(int id);

        Task<bool> HasConflictAsync(
            int salleId,
            DateTime dateDebut,
            DateTime dateFin);

        Task AddAsync(Reservation reservation);

        Task SaveChangesAsync();


    }
}
