using RoomReservationApp.Enums;
using RoomReservationApp.Services;

namespace RoomReservationApp;

/// <summary>
/// Represents the main application.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Gets the shared room reservation manager.
    /// </summary>
    public static RoomReservationManager ReservationManager { get; } = new();

    /// <summary>
    /// Initializes the application.
    /// </summary>
    public App()
    {
        InitializeComponent();

        AddDefaultMeetingRooms();
    }

    /// <summary>
    /// Adds the initial meeting rooms to the application.
    /// </summary>
    private static void AddDefaultMeetingRooms()
    {
        if (ReservationManager.MeetingRooms.Count > 0)
        {
            return;
        }

        ReservationManager.AddMeetingRoom(
            "S105",
            20,
            RoomLayoutType.HollowSquare,
            "hollowsquare_icon.svg");

        ReservationManager.AddMeetingRoom(
            "S106",
            30,
            RoomLayoutType.UShape,
            "ushape_icon.svg");

        ReservationManager.AddMeetingRoom(
            "S107",
            40,
            RoomLayoutType.Classroom,
            "classroom_icon.svg");

        ReservationManager.AddMeetingRoom(
            "S108",
            100,
            RoomLayoutType.Auditorium,
            "auditorium_icon.svg");
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}