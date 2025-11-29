
namespace MorskoyGoy.EnhancedNetworking
{
    using System;

    public class ListenerThreadArgs : NetworkingThreadArgs
    {
        public ListenerThreadArgs(int pollDelay = 200) : base(pollDelay)
        {
        }
    }
}