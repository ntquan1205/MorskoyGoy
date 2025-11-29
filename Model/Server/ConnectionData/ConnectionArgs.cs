
namespace MorskoyGoy.Model
{
    using EnhancedNetworking;


    public class ConnectionArgs
    {

        public ClientState ClientState
        {
            get;
            set;
        }


        public string UserName
        {
            get;
            set;
        }

        public IConnection Challenger
        {
            get;
            set;
        }


        public int Id
        {
            get;
            set;
        }
    }
}