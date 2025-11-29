
namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.Threading;

    public abstract class NetworkTask
    {
  
        private Thread networkTaskThread;

  
        private NetworkTaskThreadArgs networkTaskThreadArgs;

        public NetworkTask(IConnection connection, int intervalTime = 1000)
        {
            this.Connection = connection ?? throw new ArgumentNullException(nameof(connection), "The value must not be null.");
            this.networkTaskThread = new Thread(this.Worker);
            this.networkTaskThreadArgs = new NetworkTaskThreadArgs(intervalTime);
        }

        protected IConnection Connection
        {
            get;
            set;
        }

        public void Start(object parameter, bool repeat)
        {
            if (!repeat)
            {
                this.Execute(parameter);
                return;
            }

            if (this.networkTaskThread.ThreadState == ThreadState.Unstarted)
            {
                this.networkTaskThreadArgs.NetworkTaskParameter = parameter;
                this.networkTaskThread.Start(this.networkTaskThreadArgs);
            }
        }

        public void Stop()
        {
            this.networkTaskThreadArgs.Stop = true;
        }

        protected abstract void Execute(object parameter);


        private void Worker(object data)
        {
            NetworkTaskThreadArgs args = (NetworkTaskThreadArgs)data;

            while (!args.Stop)
            {
                this.Execute(args.NetworkTaskParameter);
                Thread.Sleep(args.PollDelay);
            }
        }
    }
}
