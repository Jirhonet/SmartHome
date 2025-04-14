using Microsoft.AspNetCore.Components;
using SmartHome.Models;
using SmartHome.Services;

namespace SmartHome.Components.Pages.Home
{
    public partial class Home
    {
        [Inject]
        private RoomService RoomService { get; set; }

        private List<Room> rooms;
        private Room selectedRoom;
        private string searchTerm;
        private bool showDialog;
        private Room dialogRoom;

        protected override async Task OnInitializedAsync()
        {
            await LoadRoomsAsync();
        }

        private async Task LoadRoomsAsync()
        {
            rooms = await RoomService.GetAsync(searchTerm);
        }

        private void SelectRoom(Room room)
        {
            selectedRoom = room;
        }

        private async Task ToggleAllLightsAsync(int id)
        {
            await RoomService.ToggleAllLightsAsync(id);
            await LoadRoomsAsync();
        }

        private async Task ToggleAllSpeakersAsync(int id)
        {
            await RoomService.ToggleAllSpeakersAsync(id);
            await LoadRoomsAsync();
        }

        private void CreateNewRoom()
        {
            // Set new light default values
            dialogRoom = new Room
            {
                Name = "New Room",
            };
            showDialog = true;
        }

        private void EditSelectedRoom()
        {
            if (selectedRoom != null)
            {
                dialogRoom = new Room
                {
                    Id = selectedRoom.Id,
                    Name = selectedRoom.Name,
                };
                showDialog = true;
            }
        }

        private async Task DeleteSelectedRoom()
        {
            if (selectedRoom != null)
            {
                await RoomService.DeleteAsync(selectedRoom.Id);
                selectedRoom = null;
                await LoadRoomsAsync();
            }
        }

        private async Task HandleRoomSave(Room room)
        {
            if (room.Id == 0)
            {
                await RoomService.CreateAsync(room);
            }
            else
            {
                await RoomService.UpdateAsync(room);
            }
            await LoadRoomsAsync();
        }

        private async Task UpdateSearchTerm()
        {
            await LoadRoomsAsync();
        }

        private async Task ClearSearch()
        {
            searchTerm = "";
            await LoadRoomsAsync();
        }
    }
}