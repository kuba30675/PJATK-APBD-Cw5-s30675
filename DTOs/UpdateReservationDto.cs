using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class UpdateReservationDto : IValidatableObject
{
    public int RoomId { get; set; }
    [Required] public string OrganizerName { get; set; } = string.Empty;
    [Required] public string Topic { get; set; } = string.Empty;
    public DateOnly StartTime { get; set; }
    public DateOnly EndTime { get; set; }
    public string Status { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "End Time must be greater than start time",
                new[] { nameof(EndTime) }
            );
        }
    }
}