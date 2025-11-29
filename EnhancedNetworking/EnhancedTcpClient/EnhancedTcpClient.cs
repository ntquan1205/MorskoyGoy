namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class EnhancedTcpClient
    {
  
        private const int PollDelay = 20;


        private Thread listenerThread;

  
        private ListenerThreadArgs listenerThreadArgs;

        private bool connectionLost;

    
        public EnhancedTcpClient()
        {
            this.listenerThread = new Thread(this.ListenerWorker);
            this.listenerThreadArgs = new ListenerThreadArgs(PollDelay);
        }


        public EnhancedTcpClient(TcpClient client)
        {
            this.Client = client;
            this.listenerThread = new Thread(this.ListenerWorker);
            this.listenerThreadArgs = new ListenerThreadArgs(PollDelay);
        }

        public EnhancedTcpClient(IPEndPoint ipEndPoint)
        {
            this.Client = new TcpClient();
            try
            {
                this.Client.Connect(ipEndPoint);
            }
            catch
            {
                this.FireConnectionFailed(this, EventArgs.Empty);
            }

            this.listenerThread = new Thread(this.ListenerWorker);
            this.listenerThreadArgs = new ListenerThreadArgs(PollDelay);
        }

 
        public event EventHandler<DataReceivedEventArgs> DataReceived;

  
        public event EventHandler<EventArgs> ConnectionLost;

   
        public event EventHandler<EventArgs> ConnectionFailed;


        public TcpClient Client
        {
            get;
            private set;
        }

 
        public bool Connected
        {
            get
            {
                if (this.Client == null)
                {
                    return false;
                }

                return this.Client.Connected;
            }
        }

        public NetworkStream Stream
        {
            get
            {
                return this.Client.GetStream();
            }
        }


        public IPEndPoint IPEndPoint
        {
            get
            {
                return (IPEndPoint)this.Client.Client.RemoteEndPoint;
            }
        }

        public void Connect(IPEndPoint ipEndpoint)
        {
            this.Client = new TcpClient();
            try
            {
                this.Client.Connect(ipEndpoint);
            }
            catch
            {
                this.FireConnectionFailed(this, EventArgs.Empty);
            }
        }


        public void Close()
        {
            this.StopListening();
            this.Client.Close();
        }

        public void Write(byte[] data)
        {
            if (!this.Client.Connected)
            {
                this.FireConnectionLost(this, EventArgs.Empty);
                return;
            }

            try
            {
                this.Stream.Write(data, 0, data.Length);
            }
            catch
            {
                this.FireConnectionLost(this, EventArgs.Empty);
            }
        }

        public void Write(string text)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text);

            try
            {
                this.Stream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                this.FireConnectionLost(this, EventArgs.Empty);
            }
        }

        public void StartListening()
        {
            this.listenerThreadArgs.Stop = false;

            if (this.listenerThread.ThreadState == ThreadState.Unstarted)
            {
                this.listenerThread.Start(this.listenerThreadArgs);
            }
        }


        public void StopListening()
        {
            this.listenerThreadArgs.Stop = true;
        }

        protected virtual void FireDataReceived(object sender, DataReceivedEventArgs args)
        {
            if (this.DataReceived != null)
            {
                this.DataReceived(sender, args);
            }
        }

        protected virtual void FireConnectionLost(object sender, EventArgs args)
        {
            this.listenerThreadArgs.Stop = true;

            if (this.ConnectionLost != null && this.connectionLost == false)
            {
                this.connectionLost = true;
                this.ConnectionLost(sender, args);
            }
        }

        protected virtual void FireConnectionFailed(object sender, EventArgs args)
        {
            if (this.ConnectionFailed != null)
            {
                this.ConnectionFailed(sender, args);
            }
        }

        private void ListenerWorker(object data)
        {
            ListenerThreadArgs args = (ListenerThreadArgs)data;
            byte[] receiveBuffer = new byte[8192];
            int receivedBytes = 0;

            while (!args.Stop)
            {
                try
                {
                    receivedBytes = this.Stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                }
                catch
                {
                    this.FireConnectionLost(this, EventArgs.Empty);
                    return;
                }

                if (receivedBytes > 0)
                {
                    this.FireDataReceived(this, new DataReceivedEventArgs(receiveBuffer.Take(receivedBytes).ToArray()));
                }

                Thread.Sleep(args.PollDelay);
            }
        }
    }
}
