
namespace MorskoyGoy.Model
{
    using System;
    using EnhancedNetworking;
    public class BattleReadyEventArgs : EventArgs
    {

        public BattleReadyEventArgs(IConnection competitorA, IConnection competitorB)
        {
            this.CompetitorA = competitorA ?? throw new ArgumentNullException(nameof(competitorA), "The value must not be null.");
            this.CompetitorB = competitorB ?? throw new ArgumentNullException(nameof(competitorB), "The value must not be null.");
        }

        public IConnection CompetitorA
        {
            get;
            private set;
        }
        public IConnection CompetitorB
        {
            get;
            private set;
        }
    }
}