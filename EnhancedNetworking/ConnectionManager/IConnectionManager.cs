
namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.Collections.Generic;


    public interface IConnectionManager
    {
  
        event EventHandler<ClientAcceptedEventArgs> ConnectionAccepted;

        event EventHandler<MessageReceivedEventArgs> MessageReceived;


        event EventHandler<DataReceivedEventArgs> RawDataReceived;

 
        List<IConnection> Connections { get; }

        void Start();
        void Stop();
    }
}