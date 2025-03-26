using SmartHome.Models;
using SmartHome.Repositories;

namespace SmartHome.Services
{
    public class LightService
    {
        private readonly LightRepository repository;

        public LightService(LightRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<List<Light>> GetAsync(string search = null, CancellationToken ct = default)
        {
            return repository.GetAsync(search, ct);
        }

        public Task CreateAsync(Light light, CancellationToken ct = default)
        {
            return repository.InsertAsync(light, ct);
        }

        public Task UpdateAsync(Light light, CancellationToken ct = default)
        {
            return repository.UpdateAsync(light, ct);
        }

        public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return repository.DeleteAsync(id, ct);
        }

        public async Task ToggleIsOnAsync(int id, CancellationToken ct = default)
        {
            Light light = await repository.GetByIdAsync(id, ct);

            if (light != null)
            {
                light.Toggle();
                await repository.UpdateAsync(light, ct);
            }
        }
    }
}
