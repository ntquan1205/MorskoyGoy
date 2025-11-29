
namespace MorskoyGoy.Model
{
    using System;
    using EnhancedNetworking;

    public class Battle
    {

        public Battle(IConnection competitorA, IConnection competitorB)
        {
            if (competitorA == null)
            {
                throw new ArgumentNullException(nameof(competitorA), "The value must not be null.");
            }

            if (competitorB == null)
            {
                throw new ArgumentNullException(nameof(competitorB), "The value must not be null.");
            }

            this.CompetitorA = new Competitor(competitorA);
            this.CompetitorB = new Competitor(competitorB);

            this.CompetitorA.ShipPositionsReceived += this.Competitor_ShipPositionsReceived;
            this.CompetitorB.ShipPositionsReceived += this.Competitor_ShipPositionsReceived;

            this.CompetitorA.LeftGame += this.Competitor_LeftGame;
            this.CompetitorB.LeftGame += this.Competitor_LeftGame;

            try
            {
                this.CompetitorA.SendInitiateBattle();
                this.CompetitorB.SendInitiateBattle();
            }
            catch
            {
                throw;
            }
        }


        public event EventHandler<FinishedArgs> Finished;

        public Competitor CompetitorA
        {
            get;
            private set;
        }

        public Competitor CompetitorB
        {
            get;
            private set;
        }

        protected virtual void FireFinished(object sender, FinishedArgs args)
        {
            this.CompetitorA.Connection.Close();
            this.CompetitorB.Connection.Close();

            if (this.Finished != null)
            {
                this.Finished(sender, args);
            }
        }

        private void Competitor_ShipPositionsReceived(object sender, ShipPositionsReceivedEventArgs args)
        {
            Competitor competitor = (Competitor)sender;
            competitor.BattleField.Ships = args.Ships;
            competitor.ShipPositionsReceived -= this.Competitor_ShipPositionsReceived;
            competitor.PositionsReady = true;

            if (this.CompetitorB.PositionsReady && this.CompetitorA.PositionsReady)
            {
                this.InitiateShooting();
            }
        }

        private void InitiateShooting()
        {
            this.CompetitorA.SendMoveRequest();
            this.CompetitorA.MoveReceived += this.Competitor_MoveReceived;
        }

        private void Competitor_MoveReceived(object sender, MoveReceivedEventArgs args)
        {
            Competitor competitor = (Competitor)sender;
            Competitor opponent = this.Opponent(competitor);

            competitor.MoveReceived -= this.Competitor_MoveReceived;

            object marker = new Marker(); 
            bool validMove = true; 

            if (!validMove)
            {
                opponent.SendGameWon();
                competitor.Connection.Close();
                this.FireFinished(this, new FinishedArgs(this, opponent, competitor));
                return;
            }

            competitor.SendMoveReport(marker as Marker);
            opponent.SendOpponentsMove(marker as Marker);

            
        }
        private void Competitor_LeftGame(object sender, EventArgs e)
        {
            Competitor quitter = (Competitor)sender;
            quitter.LeftGame -= this.Competitor_LeftGame;
            Competitor other = this.Opponent(quitter);
            other.SendGameWon();
            this.FireFinished(this, new FinishedArgs(this, other, quitter));
        }

        private Competitor Opponent(Competitor competitor)
        {
            if (competitor == this.CompetitorA)
            {
                return this.CompetitorB;
            }

            return this.CompetitorA;
        }
    }
}