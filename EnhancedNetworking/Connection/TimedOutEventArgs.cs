
namespace MorskoyGoy.EnhancedNetworking
{
    public class TimedOutEventArgs : DisconnectedEventArgs
    {
        public TimedOutEventArgs(IConnection connection) : base(connection)
        {
        }
    }
}
