
namespace MorskoyGoy.EnhancedNetworking
{

    public class TimerThreadArgs
    {

        public TimerThreadArgs(int limit, bool loop)
        {
            this.Limit = limit;
            this.Loop = loop;
            this.Stop = false;
        }


        public bool Stop
        {
            get;
            set;
        }

        public int Limit
        {
            get;
            private set;
        }

        public bool Loop
        {
            get;
            private set;
        }
    }
}