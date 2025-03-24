using SmartHome.Models;
using SmartHome.Services;

namespace SmartHome.Components.Pages
{
    public partial class Lights
    {
        private IEnumerable<Light> lights;
        private Light selectedLight;
        private string searchTerm = string.Empty;

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
            lights = await LightService.GetAsync(searchTerm, ct);
        }

        private void SelectLight(Light light)
        {
            selectedLight = selectedLight?.Id == light.Id ? null : light;
        }

        private void CreateNewLight()
        {
            // TODO: Implement new light creation
        }

        private void EditSelectedLight()
        {
            if (selectedLight != null)
            {
                // TODO: Implement light editing
            }
        }

        private async Task DeleteSelectedLight()
        {
            if (selectedLight != null)
            {
                // TODO: Implement light deletion
                // await LightService.DeleteAsync(selectedLight.Id);
                // await LoadLightsAsync();
                selectedLight = null;
            }
        }

        private async void UpdateSearchTerm()
        {
            try
            {
                await LoadLightsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ClearSearch()
        {
            searchTerm = string.Empty;
        }
    }
}