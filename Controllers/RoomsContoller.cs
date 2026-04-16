using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetAll([FromQuery] char? buildingCode, int? floor, int? capacity,
        bool? hasProjector, bool? activeOnly)
    {
        var rooms = Database.RoomsStorage;
        var res = rooms.Where(r => buildingCode is null || r.BuildingCode == buildingCode)
            .Where(r => floor is null || r.Floor == floor)
            .Where(r => capacity is null || r.Capacity == capacity)
            .Where(r => hasProjector is null || r.HasProjector == hasProjector)
            .Where(r => activeOnly is null || r.IsActive == activeOnly).ToList();
        if (!res.Any())
            return NotFound();

        return Ok(res.Select(room => new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector
        }));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetRoomById(int id)
    {
        var room = Database.RoomsStorage.FirstOrDefault(r => r.Id == id);
        if (room is null)
            return NotFound();

        return Ok(new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector
        });
    }

    [HttpGet("bulding/{buildingCode}")]
    public IActionResult GetRoomsByBuildingCode(char buildingCode)
    {
        var rooms = Database.RoomsStorage.Where(r => r.BuildingCode == buildingCode).ToList();
        if (rooms.Count() == 0)
            return NotFound("No rooms with such bulding code!");

        return Ok(
            rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                Name = r.Name,
                BuildingCode = r.BuildingCode,
                Floor = r.Floor,
                Capacity = r.Capacity,
                HasProjector = r.HasProjector
            })
        );
    }

    [HttpPost]
    public IActionResult AddRoom(CreateRoomDto dto)
    {
        var room = new Room
        {
            Id = Database.RoomsStorage.Max(r => r.Id) + 1,
            Name = dto.Name,
            BuildingCode = dto.BuildingCode,
            Floor = dto.Floor,
            Capacity = dto.Capacity,
            HasProjector = dto.HasProjector,
            IsActive = true
        };
        Database.RoomsStorage.Add(room);

        return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateRoom(UpdateRoomDto dto, int id)
    {
        var room = Database.RoomsStorage.FirstOrDefault(r => r.Id == id);
        if (room is null)
            return NotFound($"There is no room with such ID: {id}");

        room.Name = dto.Name;
        room.BuildingCode = dto.BuildingCode;
        room.Capacity = dto.Capacity;
        room.Floor = dto.Floor;
        room.HasProjector = dto.HasProjector;
        room.IsActive = dto.IsActive;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = Database.RoomsStorage.FirstOrDefault(r => r.Id == id);

        if (room is null)
            return NotFound($"There is no room with such ID: {id}");

        Database.RoomsStorage.Remove(room);
        return NoContent();
    }
    
    
}