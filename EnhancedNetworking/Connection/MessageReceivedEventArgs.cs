namespace MorskoyGoy.EnhancedNetworking
{
    using System;

    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(object message)
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message), "The value must not be null.");
        }
        public MessageReceivedEventArgs(object message, IConnection connection)
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message), "The value must not be null.");
            this.Connection = connection ?? throw new ArgumentNullException(nameof(connection), "The value must not be null.");
        }

        public IConnection Connection
        {
            get;
            private set;
        }

        public object Message
        {
            get;
            private set;
        }
    }
}