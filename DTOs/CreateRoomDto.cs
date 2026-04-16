using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CreateRoomDto
{
    [StringLength(30, MinimumLength = 3), Required] public string Name { get; set; } = string.Empty;
    [Required] public char BuildingCode { get; set; }
    [Required] public int Floor { get; set; }
    [Required, Range(1, 35)] public int Capacity { get; set; }
    [Required] public bool HasProjector { get; set; }
}