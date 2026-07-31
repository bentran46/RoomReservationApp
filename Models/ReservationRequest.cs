using RoomReservationApp.Enums;

namespace RoomReservationApp.Models;

/// <summary>
/// Represents a request to reserve a meeting room.
/// </summary>
public class ReservationRequest
{
    private string _requestedBy = string.Empty;
    private string _description = string.Empty;
    private DateTime _startDateTime;
    private DateTime _endDateTime;
    private int _participantCount;

    /// <summary>
    /// Gets the unique ID of the reservation request.
    /// </summary>
    public int RequestID { get; }

    /// <summary>
    /// Gets or sets the name of the person making the request.
    /// </summary>
    public string RequestedBy
    {
        get => _requestedBy;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Requested by is required.");
            }

            _requestedBy = value;
        }
    }

    /// <summary>
    /// Gets or sets the purpose of the meeting.
    /// </summary>
    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Description is required.");
            }

            _description = value;
        }
    }

    /// <summary>
    /// Gets or sets the start date and time of the meeting.
    /// </summary>
    public DateTime StartDateTime
    {
        get => _startDateTime;
        set
        {
            if (value <= DateTime.Now)
            {
                throw new ArgumentException(
                    "Start date and time must be in the future.");
            }

            _startDateTime = value;
        }
    }

    /// <summary>
    /// Gets or sets the end date and time of the meeting.
    /// </summary>
    public DateTime EndDateTime
    {
        get => _endDateTime;
        set
        {
            if (value <= StartDateTime)
            {
                throw new ArgumentException(
                    "End date and time must be after the start date and time.");
            }

            _endDateTime = value;
        }
    }

    /// <summary>
    /// Gets or sets the estimated number of participants.
    /// </summary>
    public int ParticipantCount
    {
        get => _participantCount;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException(
                    "Participant count must be greater than zero.");
            }

            _participantCount = value;
        }
    }

    /// <summary>
    /// Gets or sets the current status of the reservation request.
    /// </summary>
    public RequestStatus RequestStatus { get; set; }

    /// <summary>
    /// Gets the meeting room associated with the request.
    /// </summary>
    public MeetingRoom MeetingRoom { get; }

    /// <summary>
    /// Initializes a new reservation request.
    /// </summary>
    public ReservationRequest(
        int requestId,
        string requestedBy,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        int participantCount,
        MeetingRoom meetingRoom)
    {
        if (requestId <= 0)
        {
            throw new ArgumentException(
                "Request ID must be greater than zero.");
        }

        MeetingRoom = meetingRoom
            ?? throw new ArgumentNullException(nameof(meetingRoom));

        RequestID = requestId;
        RequestedBy = requestedBy;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        ParticipantCount = participantCount;
        RequestStatus = RequestStatus.Pending;
    }

    /// <summary>
    /// Returns a summary of the reservation request.
    /// </summary>
    public override string ToString()
    {
        return $"Request #{RequestID}: {Description} - {RequestedBy}";
    }
}