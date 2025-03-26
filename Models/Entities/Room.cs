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
        public List<IDevice> Devices { get; set; }

        /// <summary>
        /// Toggles all lights in the room.
        /// </summary>
        public void ToggleAllLights()
        {
            foreach (Light light in Devices.OfType<Light>())
            {
                light.Toggle();
            }
        }

        /// <summary>
        /// Toggles all speakers in the room.
        /// </summary>
        public void ToggleAllSpeakers()
        {
            foreach (Speaker speaker in Devices.OfType<Speaker>())
            {
                speaker.Toggle();
            }
        }
    }
}
