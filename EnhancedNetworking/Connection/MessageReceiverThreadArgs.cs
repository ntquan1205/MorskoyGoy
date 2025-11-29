namespace MorskoyGoy.EnhancedNetworking
{

    public class MessageReceiverThreadArgs : NetworkingThreadArgs
    {
        public MessageReceiverThreadArgs(int pollDelay = 200) : base(pollDelay)
        {
        }
    }
}
