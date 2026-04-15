using WebApplication1.Models;

namespace WebApplication1;

public static class Database
{
    public static readonly List<Room> RoomsStorage = new List<Room>
    {
        new Room
        {
            Id = 1,
            Name = "Room A1",
            BuildingCode = 'A',
            Floor = 1,
            Capacity = 10,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Room A2",
            BuildingCode = 'A',
            Floor = 2,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Room B1",
            BuildingCode = 'B',
            Floor = 1,
            Capacity = 15,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Room B3",
            BuildingCode = 'B',
            Floor = 3,
            Capacity = 30,
            HasProjector = true,
            IsActive = false
        },
        new Room
        {
            Id = 5,
            Name = "Room C2",
            BuildingCode = 'C',
            Floor = 2,
            Capacity = 25,
            HasProjector = false,
            IsActive = true
        }
    };

    public static readonly List<Reservation> ReservationsStorage = new List<Reservation>
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Kuba",
            Topic = "AI Basics",
            StartTime = new DateOnly(2026, 4, 20),
            EndTime = new DateOnly(2026, 4, 20),
            Status = "Confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Anna",
            Topic = "C# Workshop",
            StartTime = new DateOnly(2026, 4, 21),
            EndTime = new DateOnly(2026, 4, 21),
            Status = "Pending"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Piotr",
            Topic = "Math Lecture",
            StartTime = new DateOnly(2026, 4, 22),
            EndTime = new DateOnly(2026, 4, 23),
            Status = "Confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 1,
            OrganizerName = "Ola",
            Topic = "Project Meeting",
            StartTime = new DateOnly(2026, 4, 24),
            EndTime = new DateOnly(2026, 4, 24),
            Status = "Cancelled"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 5,
            OrganizerName = "Marek",
            Topic = "Startup Pitch",
            StartTime = new DateOnly(2026, 4, 25),
            EndTime = new DateOnly(2026, 4, 26),
            Status = "Confirmed"
        }
    };
}