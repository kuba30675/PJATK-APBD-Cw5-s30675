using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CreateRoomDto
{
    [MaxLength(30), Required]
    public string Name { get; set; } = string.Empty;
    [MaxLength(1), Required]
    public char BuildingCode { get; set; }
    [MaxLength(2)]
    public int Floor { get; set; }
    [MaxLength(2),Required]
    public int Capacity { get; set; }
    [Required]
    public bool HasProjector { get; set; }
}