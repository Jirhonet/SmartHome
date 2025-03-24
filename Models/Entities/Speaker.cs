using SmartHome.Enums;

namespace SmartHome.Models
{
    public sealed class Speaker : EntityModel, IDevice
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public DeviceType Type => DeviceType.Speaker;

        /// <summary>
        /// The state of the speaker.
        /// </summary>
        public SpeakerState State { get; set; }

        /// <summary>
        /// Percentage of how loud the speaker is.
        /// </summary>
        public int Volume { get; set; }
    }
}
