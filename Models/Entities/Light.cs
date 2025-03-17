using SmartHome.Enums;

namespace SmartHome.Models
{
    public sealed class Light : EntityModel, IDevice
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public DeviceType Type => DeviceType.Light;

        /// <summary>
        /// Whether the light is on or off.
        /// </summary>
        public bool IsOn { get; set; }

        /// <summary>
        /// Percentage of how bright the light is.
        /// </summary>
        public int Brightness { get; set; }
    }
}
