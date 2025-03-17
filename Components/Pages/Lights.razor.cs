using SmartHome.Models;
using SmartHome.Services;

namespace SmartHome.Components.Pages
{
    public partial class Lights
    {
        private IEnumerable<Light> lights;

        protected override async Task OnInitializedAsync()
        {
            await LoadLightsAsync();
        }

        protected async Task ToggleLightAsync(int id, CancellationToken ct = default)
        {
            await LightService.ToggleIsOnAsync(id, ct);
            await LoadLightsAsync(ct);
        }

        private async Task LoadLightsAsync(CancellationToken ct = default)
        {
            lights = await LightService.GetAsync(ct);
        }
    }
}