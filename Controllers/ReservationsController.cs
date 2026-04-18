using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> GetAll([FromQuery] DateOnly? dateStart, DateOnly? dateEnd,
        string? status, int? roomId)
    {
        var reservations = Database.ReservationsStorage.Where(res => dateStart is null || res.StartTime == dateStart)
            .Where(res => dateEnd is null || res.EndTime == dateEnd)
            .Where(res => status is null || res.Status == status)
            .Where(res => roomId is null || res.RoomId == roomId)
            .ToList();

        if (!reservations.Any())
            return NotFound("No matching reservations for given parameters");

        return Ok(reservations.Select(res => new ReservationDto
        {
            Id = res.Id,
            RoomId = res.RoomId,
            OrganizerName = res.OrganizerName,
            Topic = res.Topic,
            StartTime = res.StartTime,
            EndTime = res.EndTime,
            Status = res.Status
        }));
    }

    [HttpGet("{id:int}")]
    public ActionResult<ReservationDto> GetReservationById(int id)
    {
        var res = Database.ReservationsStorage.FirstOrDefault(res => res.Id == id);

        if (res is null)
            return NotFound();

        return Ok(new ReservationDto
        {
            Id = res.Id,
            RoomId = res.RoomId,
            OrganizerName = res.OrganizerName,
            Topic = res.Topic,
            StartTime = res.StartTime,
            EndTime = res.EndTime,
            Status = res.Status
        });
    }

    [HttpPost]
    public IActionResult AddReservation(CreateReservationDto dto)
    {
        if (Database.ReservationsStorage.Any(res => res.RoomId == dto.RoomId && 
                                                    ((dto.StartTime >= res.StartTime && dto.StartTime <= res.EndTime) ||
                                                        (dto.EndTime >= res.StartTime && dto.EndTime <= res.EndTime) ||
                                                        (dto.StartTime <= res.StartTime && dto.EndTime >= res.EndTime))
            ))
            return Conflict();


        var room = Database.RoomsStorage.FirstOrDefault(room => room.Id == dto.RoomId);
        if (room is null)
            return Conflict($"Cannot create reservation for room ID {dto.RoomId} because it doesn't exist!");

        if (!room.IsActive)
            return Conflict($"Cannot create reservation for room ID {dto.RoomId} because it is not active!");
            
        var reservation = new Reservation
        {
            Id = Database.ReservationsStorage.Max(res => res.Id) + 1,
            RoomId = dto.RoomId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            OrganizerName = dto.OrganizerName,
            Topic = dto.Topic,
            Status = dto.Status
        };
     
        Database.ReservationsStorage.Add(reservation);

        return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateReservation(int id, UpdateReservationDto dto)
    {
        var res = Database.ReservationsStorage.FirstOrDefault(res => res.Id == id);

        if (res is null)
            return NotFound($"No reservation with ID {id}");

        res.RoomId = dto.RoomId;
        res.StartTime = dto.StartTime;
        res.EndTime = dto.EndTime;
        res.OrganizerName = dto.OrganizerName;
        res.Topic = dto.Topic;
        res.Status = dto.Status;

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteReservation(int id)
    {
        var res = Database.ReservationsStorage.FirstOrDefault(res => res.Id == id);

        if (res is null)
            return NotFound($"No reservation with ID {id}");

        Database.ReservationsStorage.Remove(res);
        return NoContent();
    }
    
    
}