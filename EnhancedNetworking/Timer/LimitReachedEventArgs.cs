
namespace MorskoyGoy.EnhancedNetworking
{
    using System;


    public class LimitReachedEventArgs : EventArgs
    {
 
        public LimitReachedEventArgs()
        {
            this.Timestamp = DateTime.Now;
        }
        public DateTime Timestamp
        {
            get;
            private set;
        }
    }
}