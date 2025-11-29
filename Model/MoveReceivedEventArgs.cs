
namespace MorskoyGoy.Model
{
    using System;

    public class MoveReceivedEventArgs : EventArgs
    {
        public MoveReceivedEventArgs(Position position)
        {
            this.Move = position ?? throw new ArgumentNullException(nameof(position), "The value must not be null.");
        }

        public MoveReceivedEventArgs(Marker marker)
        {
            this.Marker = marker ?? throw new ArgumentNullException(nameof(marker), "The value must not be null.");
        }

        public Marker Marker
        {
            get;
            private set;
        }

        public Position Move
        {
            get;
            private set;
        }
    }
}