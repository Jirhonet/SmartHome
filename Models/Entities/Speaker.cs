﻿using SmartHome.Enums;

namespace SmartHome.Models
{
    public sealed class Speaker : EntityModel, IDevice
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public int? RoomId { get; set; }

        /// <inheritdoc />
        public string RoomName { get; set; }

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

        /// <summary>
        /// Toggles if the speaker is on or off.
        /// </summary>
        public void Toggle(SpeakerState? state = null)
        {
            state ??= State == SpeakerState.Off
                    ? SpeakerState.On
                    : SpeakerState.Off;

            State = state.Value;
        }

        /// <summary>
        /// Plays or pauses the speaker.
        /// </summary>
        public void PlayPause()
        {
            State = State == SpeakerState.Playing
                    ? SpeakerState.Paused
                    : SpeakerState.Playing;
        }
    }
}
