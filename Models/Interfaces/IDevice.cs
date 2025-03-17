using SmartHome.Enums;

namespace SmartHome.Models
{
    public interface IDevice
    {
        /// <summary>
        /// The name of the device.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the device.
        /// </summary>
        public DeviceType Type { get; }
    }
}
