using RoomReservationApp.Enums;
using RoomReservationApp.Models;

namespace RoomReservationApp.Services;

/// <summary>
/// Manages meeting rooms and reservation requests.
/// </summary>
public class RoomReservationManager
{
    private readonly List<MeetingRoom> _meetingRooms = [];
    private readonly List<ReservationRequest> _reservationRequests = [];

    /// <summary>
    /// Gets all meeting rooms.
    /// </summary>
    public IReadOnlyList<MeetingRoom> MeetingRooms => _meetingRooms;

    /// <summary>
    /// Gets all reservation requests.
    /// </summary>
    public IReadOnlyList<ReservationRequest> ReservationRequests =>
        _reservationRequests;

    /// <summary>
    /// Adds a new meeting room if its room number is not already in use.
    /// </summary>
    public MeetingRoom AddMeetingRoom(
        string roomNumber,
        int seatingCapacity,
        RoomLayoutType roomLayoutType,
        string roomImageFileName)
    {
        bool duplicateRoomExists = _meetingRooms.Any(
            room => room.RoomNumber.Equals(
                roomNumber,
                StringComparison.OrdinalIgnoreCase));

        if (duplicateRoomExists)
        {
            throw new ArgumentException(
                $"Room number {roomNumber} already exists.");
        }

        MeetingRoom meetingRoom = new(
            roomNumber,
            seatingCapacity,
            roomLayoutType,
            roomImageFileName);

        _meetingRooms.Add(meetingRoom);

        return meetingRoom;
    }

    /// <summary>
    /// Adds a reservation request for an existing meeting room.
    /// </summary>
    public ReservationRequest AddReservationRequest(
        string roomNumber,
        string requestedBy,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        int participantCount)
    {
        MeetingRoom? selectedRoom = _meetingRooms.FirstOrDefault(
            room => room.RoomNumber.Equals(
                roomNumber,
                StringComparison.OrdinalIgnoreCase));

        if (selectedRoom is null)
        {
            throw new ArgumentException(
                $"Meeting room {roomNumber} does not exist.");
        }

        if (participantCount > selectedRoom.SeatingCapacity)
        {
            throw new ArgumentException(
                $"Room {roomNumber} can only accommodate " +
                $"{selectedRoom.SeatingCapacity} participants.");
        }

        int nextRequestId = _reservationRequests.Count + 1;

        ReservationRequest reservationRequest = new(
            nextRequestId,
            requestedBy,
            description,
            startDateTime,
            endDateTime,
            participantCount,
            selectedRoom);

        _reservationRequests.Add(reservationRequest);

        return reservationRequest;
    }

    /// <summary>
    /// Returns all reservation requests for the selected room.
    /// </summary>
    public List<ReservationRequest> GetRequestsForRoom(string roomNumber)
    {
        return _reservationRequests
            .Where(request =>
                request.MeetingRoom.RoomNumber.Equals(
                    roomNumber,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}