using RoomReservationApp.Models;

namespace RoomReservationApp.Views;

public partial class ViewRequestsPage : ContentPage
{
    private readonly MeetingRoom _selectedRoom;

    public ViewRequestsPage(MeetingRoom selectedRoom)
    {
        InitializeComponent();

        _selectedRoom = selectedRoom;

        RoomNumberLabel.Text =
            $"Room {_selectedRoom.RoomNumber}";

        LoadRequests();
    }

    private void LoadRequests()
    {
        RequestsCollectionView.ItemsSource =
            App.ReservationManager.GetRequestsForRoom(
                _selectedRoom.RoomNumber);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        LoadRequests();
    }

    private async void OnBackToRoomsClicked(
        object? sender,
        EventArgs e)
    {
        await Navigation.PopAsync();
    }
}