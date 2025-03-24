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

        public Task<IEnumerable<Light>> GetAsync(string search = null, CancellationToken ct = default)
        {
            return repository.GetAsync(search, ct);
        }

        public async Task ToggleIsOnAsync(int id, CancellationToken ct = default)
        {
            Light light = await repository.GetByIdAsync(id, ct);

            if (light != null)
            {
                light.IsOn = !light.IsOn;
                await repository.UpdateAsync(light, ct);
            }
        }
    }
}
