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
            $"Requests for Room {_selectedRoom.RoomNumber}";

        LoadRequests();
    }

    private void LoadRequests()
    {
        var requests =
            App.ReservationManager.GetRequestsForRoom(
                _selectedRoom.RoomNumber);

        RequestsCollectionView.ItemsSource = requests;
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