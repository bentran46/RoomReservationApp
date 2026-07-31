using RoomReservationApp.Enums;

namespace RoomReservationApp.Models;

/// <summary>
/// Represents a meeting room that can be reserved.
/// </summary>
public class MeetingRoom
{
    private string _roomNumber = string.Empty;
    private int _seatingCapacity;
    private string _roomImageFileName = string.Empty;

    /// <summary>
    /// Gets or sets the room number.
    /// </summary>
    public string RoomNumber
    {
        get => _roomNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Room number is required.");
            }

            _roomNumber = value;
        }
    }

    /// <summary>
    /// Gets or sets the seating capacity of the room.
    /// </summary>
    public int SeatingCapacity
    {
        get => _seatingCapacity;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Seating capacity must be greater than zero.");
            }

            _seatingCapacity = value;
        }
    }

    /// <summary>
    /// Gets or sets the room layout type.
    /// </summary>
    public RoomLayoutType RoomLayoutType { get; set; }

    /// <summary>
    /// Gets or sets the file name of the room image.
    /// </summary>
    public string RoomImageFileName
    {
        get => _roomImageFileName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Room image file name is required.");
            }

            _roomImageFileName = value;
        }
    }

    /// <summary>
    /// Gets the icon file name associated with the room layout.
    /// </summary>
    public string RoomTypeIcon =>
        RoomLayoutType switch
        {
            RoomLayoutType.HollowSquare => "hollowsquare_icon.png",
            RoomLayoutType.UShape => "ushape_icon.png",
            RoomLayoutType.Classroom => "classroom_icon.png",
            RoomLayoutType.Auditorium => "auditorium_icon.png",
            _ => "room_icon.png"
        };

    /// <summary>
    /// Initializes a new meeting room.
    /// </summary>
    public MeetingRoom(
        string roomNumber,
        int seatingCapacity,
        RoomLayoutType roomLayoutType,
        string roomImageFileName)
    {
        RoomNumber = roomNumber;
        SeatingCapacity = seatingCapacity;
        RoomLayoutType = roomLayoutType;
        RoomImageFileName = roomImageFileName;
    }

    /// <summary>
    /// Returns the room number and capacity.
    /// </summary>
    public override string ToString()
    {
        return $"{RoomNumber} - Capacity: {SeatingCapacity}";
    }
}
