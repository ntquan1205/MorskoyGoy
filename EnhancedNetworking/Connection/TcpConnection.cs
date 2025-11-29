namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.Net;

    public class TcpConnection : IConnection
    {
 
        private bool messageInTime;

        private Timer timeOutTimer;

        public TcpConnection()
        {
        }

        public TcpConnection(EnhancedTcpClient enhancedTcpClient)
        {
            this.EnhancedTcpClient = enhancedTcpClient;
            this.EnhancedTcpClient.ConnectionFailed += this.FireConnectionFailed;
            this.EnhancedTcpClient.ConnectionLost += this.FireConnectionLost;
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public event EventHandler<DataReceivedEventArgs> RawDataReceived;

        public event EventHandler<DisconnectedEventArgs> ConnectionLost;

        public event EventHandler<EventArgs> ConnectionFailed;

        public event EventHandler<TimedOutEventArgs> TimedOut;

        public EnhancedTcpClient EnhancedTcpClient
        {
            get;
            protected set;
        }

        public string IPAddress
        {
            get
            {
                return this.EnhancedTcpClient.IPEndPoint.Address.ToString();
            }
        }

        public object ConnectionData
        {
            get;
            set;
        }

        public int TimeoutLimit
        {
            get;
            set;
        }

        public bool Connected
        {
            get
            {
                return this.EnhancedTcpClient.Connected;
            }
        }

        public void StartListening()
        {
            this.EnhancedTcpClient.DataReceived += this.EnhancedTcpClient_DataReceived;
            this.EnhancedTcpClient.StartListening();
            this.InitializeTimeout();
        }

        public void Connect(IPEndPoint ipEndpoint)
        {
            this.EnhancedTcpClient = new EnhancedTcpClient(ipEndpoint);
            this.EnhancedTcpClient.ConnectionFailed += this.FireConnectionFailed;
            this.EnhancedTcpClient.ConnectionLost += this.FireConnectionLost;
        }
        public void Close()
        {
            if (this.EnhancedTcpClient != null)
            {
                this.EnhancedTcpClient.Close();
            }

            if (this.timeOutTimer != null)
            {
                this.timeOutTimer.Stop();
            }
        }

        public void SendMessage(object message)
        {
            try
            {
                MessageHandler.Send(message, this.EnhancedTcpClient);
            }
            catch
            {
                throw;
            }
        }

        public void SendRawData(byte[] data)
        {
            this.EnhancedTcpClient.Write(data);
        }

        protected virtual void FireRawDataReceived(object sender, DataReceivedEventArgs args)
        {
            this.RawDataReceived?.Invoke(sender, args);
        }

        protected virtual void FireMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            this.MessageReceived?.Invoke(sender, args);
        }

        protected virtual void FireConnectionLost(object sender, EventArgs args)
        {
            this.ConnectionLost?.Invoke(sender, new DisconnectedEventArgs(this));
        }

        protected virtual void FireTimedOut(object sender, TimedOutEventArgs args)
        {
            this.TimedOut?.Invoke(sender, args);
        }

        protected virtual void FireConnectionFailed(object sender, EventArgs args)
        {
            this.ConnectionFailed?.Invoke(sender, args);
        }

        private void InitializeTimeout()
        {
            if (this.TimeoutLimit > 0)
            {
                this.messageInTime = true;
                this.timeOutTimer = new Timer(this.TimeoutLimit);
                this.timeOutTimer.LimitReached += this.TimeOutTimer_LimitReached;
                this.timeOutTimer.Start();
            }
        }

        private void EnhancedTcpClient_DataReceived(object sender, DataReceivedEventArgs args)
        {
            this.messageInTime = true;
            object message = MessageHandler.Read(args.RawData);
            this.FireMessageReceived(this, new MessageReceivedEventArgs(message, this));
        }

        private void TimeOutTimer_LimitReached(object sender, LimitReachedEventArgs e)
        {
            if (!this.messageInTime)
            {
                this.FireTimedOut(this, new TimedOutEventArgs(this));
            }
            else
            {
                this.messageInTime = false;
            }
        }
    }
}
