namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.Json;
    using System.Threading;

    public class MessageReceiver
    {
        private NetworkStream networkStream;
        private Thread messageReceiverThread;
        private MessageReceiverThreadArgs messageReceiverThreadArgs;
        private byte[] buffer;

        public MessageReceiver(NetworkStream networkStream)
        {
            this.networkStream = networkStream ?? throw new ArgumentNullException(nameof(networkStream), "The value must not be null.");
            this.messageReceiverThreadArgs = new MessageReceiverThreadArgs();
            this.messageReceiverThread = new Thread(this.Worker);
            this.buffer = new byte[8192];
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public void Start()
        {
            if (this.messageReceiverThread.ThreadState == ThreadState.Unstarted)
            {
                this.messageReceiverThread.Start(this.messageReceiverThreadArgs);
            }
        }

        protected virtual void FireMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            this.MessageReceived?.Invoke(sender, args);
        }

        private void Worker(object data)
        {
            MessageReceiverThreadArgs args = (MessageReceiverThreadArgs)data;

            while (!args.Stop)
            {
                try
                {
                    int bytesRead = this.networkStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        MessageContainer container = JsonSerializer.Deserialize<MessageContainer>(json);
                        this.FireMessageReceived(this, new MessageReceivedEventArgs(container.Content));
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    break;
                }

                Thread.Sleep(args.PollDelay);
            }
        }
    }
}