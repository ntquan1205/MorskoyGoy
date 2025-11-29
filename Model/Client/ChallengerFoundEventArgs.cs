
namespace MorskoyGoy.Model
{
    using System;

    public class ChallengerFoundEventArgs : EventArgs
    {

        public ChallengerFoundEventArgs(ClientDummy challengerInfo)
        {
            this.ChallengerInfo = challengerInfo ?? throw new ArgumentNullException(nameof(challengerInfo), "The value must not be null.");
        }
        public ClientDummy ChallengerInfo
        {
            get;
            private set;
        }
    }
}
