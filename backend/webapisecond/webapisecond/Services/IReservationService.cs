using webapisecond.DTOs;

namespace webapisecond.Services
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllAsync(
            int? salleId,
            DateTime? date);

        Task<ReservationDto?> GetByIdAsync(int id);

        Task<ReservationDto> CreateAsync(
            CreateReservationDto dto);

        Task<bool> AnnulerAsync(int id);
    }
}