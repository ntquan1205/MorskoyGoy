namespace MorskoyGoy.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using EnhancedNetworking;


    public class BattleManager
    {
 
        private List<IConnection> usersInLobby;

        public BattleManager(List<IConnection> usersInLobby)
        {
            this.Battles = new List<Battle>();
            this.usersInLobby = usersInLobby;
        }

        public List<Battle> Battles
        {
            get;
            private set;
        }
        public void InitiateBattle(IConnection competitorA, IConnection competitorB)
        {
            ConnectionArgs argsA = (ConnectionArgs)competitorA.ConnectionData;
            ConnectionArgs argsB = (ConnectionArgs)competitorB.ConnectionData;
            argsA.ClientState = ClientState.InGame;
            argsB.ClientState = ClientState.InGame;
            Battle battle = new Battle(competitorA, competitorB);
            battle.Finished += this.Battle_Finished;
            this.Battles.Add(battle);
        }
        private void Battle_Finished(object sender, FinishedArgs args)
        {
            if (this.usersInLobby.Contains(args.Loser.Connection))
            {
                this.usersInLobby.Remove(args.Loser.Connection);
            }

            if (this.usersInLobby.Contains(args.Winner.Connection))
            {
                this.usersInLobby.Remove(args.Winner.Connection);
            }

            this.Battles.Remove(args.Battle);
        }
    }
}