using Microsoft.AspNetCore.Components;
using SmartHome.Models;

namespace SmartHome.Components.Pages.Speakers;

public partial class SpeakerDialog
{
    [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public EventCallback<bool> IsVisibleChanged { get; set; }

    [Parameter]
    public EventCallback<Speaker> OnSave { get; set; }

    [Parameter]
    public Speaker Speaker { get; set; } = new();

    private async Task OnClose()
    {
        IsVisible = false;
        await IsVisibleChanged.InvokeAsync(false);
    }

    private async Task HandleSave()
    {
        await OnSave.InvokeAsync(Speaker);
        await OnClose();
    }
}