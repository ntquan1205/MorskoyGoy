
namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.Threading;


    public class Timer
    {

        private Thread timerThread;

        /// <summary>
        /// The arguments of the timer thread.
        /// </summary>
        private TimerThreadArgs timerThreadArgs;


        public Timer(int limit, bool loop = true)
        {
            this.timerThreadArgs = new TimerThreadArgs(limit, loop);
            this.timerThread = new Thread(this.WatchTime);
        }


        public event EventHandler<LimitReachedEventArgs> LimitReached;


        public bool Loop
        {
            get
            {
                return this.timerThreadArgs.Loop;
            }
        }


        public void Start()
        {
            if (this.timerThread.ThreadState == ThreadState.Unstarted)
            {
                this.timerThread.Start(this.timerThreadArgs);
            }
        }

        public void Stop()
        {
            this.timerThreadArgs.Stop = true;
        }

        protected virtual void FireLimitReached(object sender, LimitReachedEventArgs args)
        {
            if (this.LimitReached != null)
            {
                this.LimitReached(sender, args);
            }
        }

        private void WatchTime(object timerThreadArgs)
        {
            TimerThreadArgs args = (TimerThreadArgs)timerThreadArgs;

            do
            {
                Thread.Sleep(args.Limit);
                this.FireLimitReached(this, new LimitReachedEventArgs());
            }
            while (!args.Stop && args.Loop);
        }
    }
}