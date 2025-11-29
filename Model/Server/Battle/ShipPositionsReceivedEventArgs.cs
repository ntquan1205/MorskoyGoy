
namespace MorskoyGoy.Model
{
    using System;
    using System.Collections.Generic;

    public class ShipPositionsReceivedEventArgs : EventArgs
    {

        public ShipPositionsReceivedEventArgs(List<Ship> ships)
        {
            this.Ships = ships ?? throw new ArgumentNullException("The value must not be null.");
        }
        public List<Ship> Ships
        {
            get;
            private set;
        }
    }
}