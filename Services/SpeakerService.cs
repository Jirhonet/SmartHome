using SmartHome.Models;
using SmartHome.Repositories;

namespace SmartHome.Services
{
    public class SpeakerService
    {
        private readonly SpeakerRepository repository;

        public SpeakerService(SpeakerRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<List<Speaker>> GetAsync(string search = null, CancellationToken ct = default)
        {
            return repository.GetAsync(search, ct);
        }

        public Task CreateAsync(Speaker speaker, CancellationToken ct = default)
        {
            return repository.InsertAsync(speaker, ct);
        }

        public Task UpdateAsync(Speaker speaker, CancellationToken ct = default)
        {
            return repository.UpdateAsync(speaker, ct);
        }

        public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return repository.DeleteAsync(id, ct);
        }

        public async Task ToggleStateAsync(int id, CancellationToken ct = default)
        {
            Speaker speaker = await repository.GetByIdAsync(id, ct);

            if (speaker != null)
            {
                await repository.UpdateAsync(speaker, ct);
            }
        }
    }
}