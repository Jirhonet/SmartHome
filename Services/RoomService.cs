using SmartHome.Models;
using SmartHome.Repositories;

namespace SmartHome.Services
{
    public class RoomService
    {
        private readonly RoomRepository repository;
        private readonly LightRepository lightRepository;
        private readonly SpeakerRepository speakerRepository;

        public RoomService(RoomRepository repository, LightRepository lightRepository, SpeakerRepository speakerRepository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.lightRepository = lightRepository ?? throw new ArgumentNullException(nameof(lightRepository));
            this.speakerRepository = speakerRepository ?? throw new ArgumentNullException(nameof(speakerRepository));
        }

        public async Task<List<Room>> GetAsync(string search = null, CancellationToken ct = default)
        {
            List<Room> rooms = await repository.GetAsync(search, ct);
            foreach (Room room in rooms)
            {
                var lights = await lightRepository.GetByRoomIdAsync(room.Id, ct);
                room.Devices.AddRange(lights);

                var speakers = await speakerRepository.GetByRoomIdAsync(room.Id, ct);
                room.Devices.AddRange(speakers);
            }

            return rooms;
        }

        public async Task<Room> GetByIdAsync(int id, CancellationToken ct = default)
        {
            Room room = await repository.GetByIdAsync(id, ct);

            var lights = await lightRepository.GetByRoomIdAsync(room.Id, ct);
            room.Devices.AddRange(lights);

            var speakers = await speakerRepository.GetByRoomIdAsync(room.Id, ct);
            room.Devices.AddRange(speakers);

            return room;
        }

        public Task CreateAsync(Room room, CancellationToken ct = default)
        {
            return repository.InsertAsync(room, ct);
        }

        public Task UpdateAsync(Room room, CancellationToken ct = default)
        {
            return repository.UpdateAsync(room, ct);
        }

        public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            return repository.DeleteAsync(id, ct);
        }

        public async Task ToggleAllLightsAsync(int id, CancellationToken ct = default)
        {
            Room room = await GetByIdAsync(id, ct);

            if (room != null)
            {
                room.ToggleAllLights();
                foreach (Light light in room.Lights)
                    await lightRepository.UpdateAsync(light, ct);
            }
        }

        public async Task ToggleAllSpeakersAsync(int id, CancellationToken ct = default)
        {
            Room room = await GetByIdAsync(id, ct);

            if (room != null)
            {
                room.ToggleAllSpeakers();
                foreach (Speaker speaker in room.Speakers)
                    await speakerRepository.UpdateAsync(speaker, ct);
            }
        }
    }
}
