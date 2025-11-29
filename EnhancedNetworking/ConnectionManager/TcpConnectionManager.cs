namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;


    public class TcpConnectionManager : IConnectionManager
    {

        private Thread connectionManagerThread;


        private ConnectionManagerThreadArgs connectionManagerThreadArgs;


        private TcpListener tcpListener;

 
        public TcpConnectionManager(TcpListener tcpListener, int timeoutLimit)
        {
            this.tcpListener = tcpListener;
            this.connectionManagerThread = new Thread(this.Worker);
            this.connectionManagerThreadArgs = new ConnectionManagerThreadArgs();
            this.Connections = new List<IConnection>();
            this.TimeoutLimit = timeoutLimit;
        }

        public event EventHandler<ClientAcceptedEventArgs> ConnectionAccepted;


        public event EventHandler<MessageReceivedEventArgs> MessageReceived;


        public event EventHandler<DataReceivedEventArgs> RawDataReceived;

        public List<IConnection> Connections
        {
            get;
            set;
        }

        public int TimeoutLimit
        {
            get;
            private set;
        }

        public void Start()
        {
            this.tcpListener.Start();

            if (this.connectionManagerThread.ThreadState == ThreadState.Unstarted)
            {
                this.connectionManagerThread.Start(this.connectionManagerThreadArgs);
            }
        }

        public void Stop()
        {
            this.connectionManagerThreadArgs.Stop = true;
            this.tcpListener.Stop();
            for (int i = 0; i < this.Connections.Count; i++)
            {
                this.Connections[i].Close();
            }
        }

        protected void Worker(object data)
        {
            ConnectionManagerThreadArgs args = (ConnectionManagerThreadArgs)data;

            while (!args.Stop)
            {
                try
                {
                    EnhancedTcpClient client = new EnhancedTcpClient(this.tcpListener.AcceptTcpClient());

                    if (client.Connected)
                    {
                        TcpConnection connection = new TcpConnection(client);
                        connection.MessageReceived += this.FireMessageReceived;
                        connection.RawDataReceived += this.FireRawDataReceived;
                        connection.ConnectionLost += this.Connection_Disconnected;
                        connection.TimedOut += this.Connection_Disconnected;
                        connection.TimeoutLimit = this.TimeoutLimit;
                        this.Connections.Add(connection);
                        this.FireClientAccepted(this, new ClientAcceptedEventArgs(connection));
                        connection.StartListening();
                    }
                }
                catch (SocketException)
                {
                }

                Thread.Sleep(args.PollDelay);
            }
        }

        protected virtual void FireClientAccepted(object sender, ClientAcceptedEventArgs args)
        {
            this.ConnectionAccepted?.Invoke(sender, args);
        }
        protected virtual void FireRawDataReceived(object sender, DataReceivedEventArgs args)
        {
            this.RawDataReceived?.Invoke(sender, args);
        }

        protected virtual void FireMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            this.MessageReceived?.Invoke(sender, args);
        }

        private void Connection_Disconnected(object sender, DisconnectedEventArgs args)
        {
            args.Connection.Close();
            this.Connections.Remove(args.Connection);
        }
    }
}
