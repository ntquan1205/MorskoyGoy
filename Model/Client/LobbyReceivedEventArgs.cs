namespace MorskoyGoy.Model
{
    using System;
    using System.Collections.Generic;
    public class LobbyReceivedEventArgs : EventArgs
    {
        public LobbyReceivedEventArgs(List<ClientDummy> clientDummies)
        {
            this.ClientDummies = clientDummies ?? throw new ArgumentNullException(nameof(clientDummies), "The value must not be null.");
        }
        public List<ClientDummy> ClientDummies
        {
            get;
            private set;
        }
    }
}