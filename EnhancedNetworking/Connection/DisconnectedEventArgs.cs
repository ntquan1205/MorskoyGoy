namespace MorskoyGoy.EnhancedNetworking
{
    using System;

    public class DisconnectedEventArgs : EventArgs
    {
        public DisconnectedEventArgs(IConnection connection)
        {
            this.Connection = connection ?? throw new ArgumentNullException(nameof(connection), "The value must not be null.");
        }

        public IConnection Connection
        {
            get;
            protected set;
        }
    }
}