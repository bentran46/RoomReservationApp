using RoomReservationApp.Models;

namespace RoomReservationApp.Views;

public partial class AddRequestPage : ContentPage
{
    private readonly MeetingRoom _selectedRoom;

    public AddRequestPage(MeetingRoom selectedRoom)
    {
        InitializeComponent();

        _selectedRoom = selectedRoom;

        RoomImage.Source = _selectedRoom.RoomImageFileName;
        RoomNumberLabel.Text = $"Room {_selectedRoom.RoomNumber}";

        RoomDetailsLabel.Text =
            $"Capacity: {_selectedRoom.SeatingCapacity}\n" +
            $"Layout: {_selectedRoom.RoomLayoutType}";

        MeetingDatePicker.MinimumDate = DateTime.Today;
        MeetingDatePicker.Date = DateTime.Today;
    }

    private async void OnAddRequestClicked(
        object? sender,
        EventArgs e)
    {
        try
        {
            if (!int.TryParse(
                    ParticipantCountEntry.Text,
                    out int participantCount))
            {
                throw new ArgumentException(
                    "Please enter a valid participant count.");
            }

            DateTime meetingDate =
                MeetingDatePicker.Date ?? DateTime.Today;

            TimeSpan startTime =
                StartTimePicker.Time ?? TimeSpan.Zero;

            TimeSpan endTime =
                EndTimePicker.Time ?? TimeSpan.Zero;

            DateTime startDateTime =
                meetingDate.Date + startTime;

            DateTime endDateTime =
                meetingDate.Date + endTime;

            ReservationRequest request =
                App.ReservationManager.AddReservationRequest(
                    _selectedRoom.RoomNumber,
                    RequestedByEntry.Text ?? string.Empty,
                    DescriptionEntry.Text ?? string.Empty,
                    startDateTime,
                    endDateTime,
                    participantCount);

            await DisplayAlertAsync(
                "Success",
                $"Request #{request.RequestID} was added successfully.",
                "OK");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Error",
                ex.Message,
                "OK");
        }
    }

    private async void OnBackToRoomsClicked(
        object? sender,
        EventArgs e)
    {
        await Navigation.PopAsync();
    }
}