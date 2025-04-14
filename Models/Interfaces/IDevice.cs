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
        /// The id of the room where the device is located.
        /// </summary>
        public int? RoomId { get; set; }

        /// <summary>
        /// Room name where the device is located.
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// The type of the device.
        /// </summary>
        public DeviceType Type { get; }
    }
}
