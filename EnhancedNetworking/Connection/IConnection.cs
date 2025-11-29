namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.Net;

    /// <summary>
    /// Represents a connection.
    /// </summary>
    public interface IConnection
    {
  
        event EventHandler<EventArgs> ConnectionFailed;

        event EventHandler<DisconnectedEventArgs> ConnectionLost;

        event EventHandler<TimedOutEventArgs> TimedOut;

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        event EventHandler<DataReceivedEventArgs> RawDataReceived;

        string IPAddress { get; }

        object ConnectionData { get; set; }

        bool Connected { get; }

        void Connect(IPEndPoint ipEndPoint);
        void StartListening();

        void Close();
        void SendMessage(object message);
        void SendRawData(byte[] data);
    }
}