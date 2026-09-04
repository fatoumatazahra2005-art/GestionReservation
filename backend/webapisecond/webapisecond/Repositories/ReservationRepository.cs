using webapisecond.Data;
using webapisecond.Models;
using Microsoft.EntityFrameworkCore;

namespace webapisecond.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetAllAsync(
            int? salleId,
            DateTime? date)
        {
            var query = _context.Reservations
                .Include(r => r.Salle)
                .Include(r => r.Enseignant)
                .AsQueryable();

            if (salleId.HasValue)
            {
                query = query.Where(r => r.SalleId == salleId.Value);
            }

            if (date.HasValue)
            {
                query = query.Where(r => r.DateDebut.Date == date.Value.Date);
            }

            return await query.ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Salle)
                .Include(r => r.Enseignant)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> HasConflictAsync(
            int salleId,
            DateTime dateDebut,
            DateTime dateFin)
        {
            return await _context.Reservations
                .Where(r =>
                    r.SalleId == salleId &&
                    r.Statut != StatutReservation.Annulee)
                .AnyAsync(r =>
                    dateDebut < r.DateFin &&
                    dateFin > r.DateDebut);
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
