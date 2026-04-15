namespace WebApplication1.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string OrganizerName { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public DateOnly StartTime { get; set; }
    public DateOnly EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
    
}