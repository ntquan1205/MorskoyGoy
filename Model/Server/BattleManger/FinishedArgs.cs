namespace MorskoyGoy.Model
{
    using System;

    public class FinishedArgs : EventArgs
    {

        public FinishedArgs(Battle battle, Competitor winner, Competitor loser)
        {
            this.Battle = battle ?? throw new ArgumentNullException(nameof(battle), "The value must not be null.");
            this.Winner = winner ?? throw new ArgumentNullException(nameof(winner), "The value must note be null.");
            this.Loser = loser ?? throw new ArgumentNullException(nameof(winner), "The value must note be null.");
        }


        public Battle Battle
        {
            get;
            private set;
        }

   
        public Competitor Winner
        {
            get;
            private set;
        }

        public Competitor Loser
        {
            get;
            private set;
        }
    }
}