using RoomReservationApp.Models;

namespace RoomReservationApp.Views;

public partial class PickRoomPage : ContentPage
{
    private MeetingRoom? _selectedRoom;

    public PickRoomPage()
    {
        InitializeComponent();

        BindingContext = App.ReservationManager;
    }

    private void OnRoomSelectionChanged(
        object? sender,
        SelectionChangedEventArgs e)
    {
        _selectedRoom =
            e.CurrentSelection.FirstOrDefault() as MeetingRoom;

        if (_selectedRoom is null)
        {
            AddRequestButton.IsEnabled = false;
            ViewRequestsButton.IsEnabled = false;

            return;
        }

        SelectedRoomImage.Source =
            _selectedRoom.RoomImageFileName;

        SelectedRoomLabel.Text =
            $"Room {_selectedRoom.RoomNumber}";

        SelectedRoomDetailsLabel.Text =
            $"Capacity: {_selectedRoom.SeatingCapacity}\n" +
            $"Layout: {_selectedRoom.RoomLayoutType}";

        AddRequestButton.IsEnabled = true;
        ViewRequestsButton.IsEnabled = true;
    }

    private async void OnAddRequestClicked(
        object? sender,
        EventArgs e)
    {
        if (_selectedRoom is null)
        {
            await DisplayAlertAsync(
                "Room Required",
                "Please select a room first.",
                "OK");

            return;
        }

        await Navigation.PushAsync(
            new AddRequestPage(_selectedRoom));
    }

    private async void OnViewRequestsClicked(
        object? sender,
        EventArgs e)
    {
        if (_selectedRoom is null)
        {
            await DisplayAlertAsync(
                "Room Required",
                "Please select a room first.",
                "OK");

            return;
        }

        await DisplayAlertAsync(
            "Selected Room",
            $"Viewing requests for {_selectedRoom.RoomNumber}",
            "OK");
    }
}