using Microsoft.AspNetCore.Components;
using SmartHome.Models;

namespace SmartHome.Components.Pages.Lights;

public partial class LightDialog
{
    [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public EventCallback<bool> IsVisibleChanged { get; set; }

    [Parameter]
    public EventCallback<Light> OnSave { get; set; }

    [Parameter]
    public Light Light { get; set; } = new();

    private async Task OnClose()
    {
        IsVisible = false;
        await IsVisibleChanged.InvokeAsync(false);
    }

    private async Task HandleSave()
    {
        await OnSave.InvokeAsync(Light);
        await OnClose();
    }
}