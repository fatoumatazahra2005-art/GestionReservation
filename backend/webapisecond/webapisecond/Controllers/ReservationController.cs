using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapisecond.DTOs;
using webapisecond.Models;
using webapisecond.Services;

namespace webapisecond.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        // GET : api/Reservation
        [HttpGet]
        public async Task<ActionResult<List<ReservationDto>>> GetAll(
            int? salleId,
            DateTime? date)
        {
            var reservations = await _service.GetAllAsync(
                salleId,
                date
            );

            return Ok(reservations);
        }

        // GET : api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id)
        {
            var reservation = await _service.GetByIdAsync(id);

            if (reservation == null)
            {
                return NotFound(new
                {
                    message = "Réservation introuvable."
                });
            }

            return Ok(reservation);
        }

        // POST : api/Reservation
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> Create(
            CreateReservationDto dto)
        {
            try
            {
                var reservation = await _service.CreateAsync(dto);

                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // DELETE : api/Reservation/5
        [HttpPut("{id}/annuler")]
        public async Task<IActionResult> Annuler(int id)
        {
            var success = await _service.AnnulerAsync(id);
            if (!success)
            {
                return NotFound(new { message = "Réservation introuvable." });
            }
            return NoContent();
        }


    }
}