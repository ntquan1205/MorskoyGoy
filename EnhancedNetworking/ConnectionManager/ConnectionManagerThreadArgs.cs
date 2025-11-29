
namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class ConnectionManagerThreadArgs : NetworkingThreadArgs
    {
        public ConnectionManagerThreadArgs(int pollDelay = 200) : base(pollDelay)
        {
        }
    }
}
