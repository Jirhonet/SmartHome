using Microsoft.AspNetCore.Components;
using SmartHome.Models;

namespace SmartHome.Components.Pages.Home;

public partial class HomeDialog
{
    [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public EventCallback<bool> IsVisibleChanged { get; set; }

    [Parameter]
    public EventCallback<Room> OnSave { get; set; }

    [Parameter]
    public Room Room { get; set; } = new();

    private async Task OnClose()
    {
        IsVisible = false;
        await IsVisibleChanged.InvokeAsync(false);
    }

    private async Task HandleSave()
    {
        await OnSave.InvokeAsync(Room);
        await OnClose();
    }
}