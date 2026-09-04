using webapisecond.DTOs;
using webapisecond.Models;
using webapisecond.Repositories;

namespace webapisecond.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;

        public ReservationService(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ReservationDto>> GetAllAsync(
            int? salleId,
            DateTime? date)
        {
            var reservations =
                await _repository.GetAllAsync(salleId, date);

            return reservations.Select(r => new ReservationDto(
                r.Id,
                r.Salle.Nom,
                $"{r.Enseignant.Prenom} {r.Enseignant.Nom}",
                r.DateDebut,
                r.DateFin,
                r.Motif,
                r.Statut.ToString()
            )).ToList();
        }

        public async Task<ReservationDto?> GetByIdAsync(int id)
        {
            var reservation = await _repository.GetByIdAsync(id);

            if (reservation == null)
            {
                return null;
            }

            return new ReservationDto(
                reservation.Id,
                reservation.Salle.Nom,
                $"{reservation.Enseignant.Prenom} {reservation.Enseignant.Nom}",
                reservation.DateDebut,
                reservation.DateFin,
                reservation.Motif,
                reservation.Statut.ToString()
            );
        }

        public async Task<ReservationDto> CreateAsync(
            CreateReservationDto dto)
        {
            if (dto.DateFin <= dto.DateDebut)
            {
                throw new Exception(
                    "La date de fin doit être postérieure à la date de début."
                );
            }
           
            bool conflit = await _repository.HasConflictAsync(
                dto.SalleId,
                dto.DateDebut,
                dto.DateFin
            );

            if (conflit)
            {
                throw new Exception(
                    "Cette salle est déjà réservée pendant cette période."
                );
            }

          
            var reservation = new Reservation
            {
                SalleId = dto.SalleId,
                EnseignantId = dto.EnseignantId,
                DateDebut = dto.DateDebut,
                DateFin = dto.DateFin,
                Motif = dto.Motif,
                Statut = StatutReservation.Confirmee
            };

            await _repository.AddAsync(reservation);
            await _repository.SaveChangesAsync();

           
            var createdReservation =
                await _repository.GetByIdAsync(reservation.Id);

            return new ReservationDto(
                createdReservation!.Id,
                createdReservation.Salle.Nom,
                $"{createdReservation.Enseignant.Prenom} {createdReservation.Enseignant.Nom}",
                createdReservation.DateDebut,
                createdReservation.DateFin,
                createdReservation.Motif,
                createdReservation.Statut.ToString()
            );
        }

        public async Task<bool> AnnulerAsync(int id)
        {
            var reservation = await _repository.GetByIdAsync(id);

            if (reservation == null)
            {
                return false;
            }

            reservation.Statut = StatutReservation.Annulee;

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}