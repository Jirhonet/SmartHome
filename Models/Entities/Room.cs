using SmartHome.Enums;

namespace SmartHome.Models
{
    public sealed class Room : EntityModel
    {
        /// <summary>
        /// The name of the room.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The devices in the room.
        /// </summary>
        public List<IDevice> Devices { get; set; } = new List<IDevice>();

        /// <summary>
        /// The lights in the room.
        /// </summary>
        public List<Light> Lights
            => Devices.OfType<Light>()?.ToList()
                ?? new List<Light>();

        /// <summary>
        /// All speakers in the room.
        /// </summary>
        public List<Speaker> Speakers
            => Devices.OfType<Speaker>()?.ToList()
                ?? new List<Speaker>();

        /// <summary>
        /// Toggles all lights in the room. The state of the light will be the opposite of the first light.
        /// </summary>
        public void ToggleAllLights()
        {
            bool isOn = !Lights.FirstOrDefault()?.IsOn ?? false;

            foreach (Light light in Lights)
            {
                light.Toggle(isOn);
            }
        }

        /// <summary>
        /// Toggles all speakers in the room. The state of the speaker will be the opposite of the first speaker.
        /// </summary>
        public void ToggleAllSpeakers()
        {
            SpeakerState state = (Speakers.FirstOrDefault()?.State ?? SpeakerState.Off) == SpeakerState.Off
                ? SpeakerState.On
                : SpeakerState.Off;

            foreach (Speaker speaker in Speakers)
            {
                speaker.Toggle(state);
            }
        }
    }
}
