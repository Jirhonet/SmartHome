using Microsoft.AspNetCore.Components;
using SmartHome.Models;
using SmartHome.Services;

namespace SmartHome.Components.Pages.Lights
{
    public partial class Lights
    {
        [Inject]
        private LightService LightService { get; set; }

        private IEnumerable<Light> lights;
        private Light selectedLight;
        private string searchTerm;
        private bool showDialog;
        private Light dialogLight;

        protected override async Task OnInitializedAsync()
        {
            await LoadLightsAsync();
        }

        private async Task LoadLightsAsync()
        {
            lights = await LightService.GetAsync(searchTerm);
        }

        private void SelectLight(Light light)
        {
            selectedLight = light;
        }

        private async Task ToggleLightAsync(int id)
        {
            await LightService.ToggleIsOnAsync(id);
            await LoadLightsAsync();
        }

        private void CreateNewLight()
        {
            dialogLight = new Light { Name = string.Empty, Brightness = 100, IsOn = false };
            showDialog = true;
        }

        private void EditSelectedLight()
        {
            if (selectedLight != null)
            {
                dialogLight = new Light
                {
                    Id = selectedLight.Id,
                    Name = selectedLight.Name,
                    Brightness = selectedLight.Brightness,
                    IsOn = selectedLight.IsOn
                };
                showDialog = true;
            }
        }

        private async Task DeleteSelectedLight()
        {
            if (selectedLight != null)
            {
                await LightService.DeleteAsync(selectedLight.Id);
                selectedLight = null;
                await LoadLightsAsync();
            }
        }

        private async Task HandleLightSave(Light light)
        {
            if (light.Id == 0)
            {
                await LightService.CreateAsync(light);
            }
            else
            {
                await LightService.UpdateAsync(light);
            }
            await LoadLightsAsync();
        }

        private async Task UpdateSearchTerm()
        {
            await LoadLightsAsync();
        }

        private async Task ClearSearch()
        {
            searchTerm = "";
            await LoadLightsAsync();
        }
    }
}