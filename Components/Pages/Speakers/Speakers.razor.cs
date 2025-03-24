using Microsoft.AspNetCore.Components;
using SmartHome.Enums;
using SmartHome.Models;
using SmartHome.Services;

namespace SmartHome.Components.Pages.Speakers;

public partial class Speakers
{
    [Inject]
    private SpeakerService SpeakerService { get; set; }

    private List<Speaker> speakers;
    private Speaker selectedSpeaker;
    private string searchTerm;
    private bool showDialog;
    private Speaker dialogSpeaker;

    protected override async Task OnInitializedAsync()
    {
        await LoadSpeakersAsync();
    }

    private async Task LoadSpeakersAsync()
    {
        speakers = await SpeakerService.GetAsync(searchTerm);
    }

    private void SelectSpeaker(Speaker speaker)
    {
        selectedSpeaker = speaker;
    }

    private async Task ToggleSpeakerStateAsync(int id)
    {
        await SpeakerService.ToggleStateAsync(id);
        await LoadSpeakersAsync();
    }

    private void CreateNewSpeaker()
    {
        dialogSpeaker = new Speaker { Id = 0, Name = "", State = SpeakerState.Off, Volume = 50 };
        showDialog = true;
    }

    private void EditSelectedSpeaker()
    {
        if (selectedSpeaker != null)
        {
            dialogSpeaker = new Speaker
            {
                Id = selectedSpeaker.Id,
                Name = selectedSpeaker.Name,
                State = selectedSpeaker.State,
                Volume = selectedSpeaker.Volume
            };
            showDialog = true;
        }
    }

    private async Task DeleteSelectedSpeaker()
    {
        if (selectedSpeaker != null)
        {
            await SpeakerService.DeleteAsync(selectedSpeaker.Id);
            selectedSpeaker = null;
            await LoadSpeakersAsync();
        }
    }

    private async Task HandleSpeakerSave(Speaker speaker)
    {
        if (speaker.Id == 0)
        {
            await SpeakerService.CreateAsync(speaker);
        }
        else
        {
            await SpeakerService.UpdateAsync(speaker);
        }
        await LoadSpeakersAsync();
    }

    private async Task UpdateSearchTerm()
    {
        await LoadSpeakersAsync();
    }

    private async Task ClearSearch()
    {
        searchTerm = string.Empty;
        await LoadSpeakersAsync();
    }
}