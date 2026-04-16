using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CreateRoomDto
{
    [MaxLength(30), Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public char BuildingCode { get; set; }
    [Required]
    public int Floor { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public bool HasProjector { get; set; }
}