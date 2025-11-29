
namespace MorskoyGoy.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnhancedNetworking;


    public class MatchMaker
    {

        private IConnectionManager connectionManager;
        public MatchMaker(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager), "The value must not be null.");
            this.connectionManager.MessageReceived += this.ConnectionManager_MessageReceived;
            this.UsersInLobby = new List<IConnection>();
        }

        public event EventHandler<BattleReadyEventArgs> BattleIsReady;

        public List<IConnection> UsersInLobby
        {
            get;
            private set;
        }
        public List<ClientDummy> Lobby
        {
            get
            {
                List<ClientDummy> clients = new List<ClientDummy>();
                for (int i = 0; i < this.UsersInLobby.Count; i++)
                {
                    IConnection client = this.UsersInLobby[i];
                    ConnectionArgs clientData = (ConnectionArgs)client.ConnectionData;
                    bool inGame = clientData.ClientState == ClientState.InGame ? true : false;
                    clients.Add(new ClientDummy(clientData.UserName, client.IPAddress, inGame, clientData.Id));
                }

                return clients;
            }
        }

        protected virtual void FireBattleReady(object sender, BattleReadyEventArgs args)
        {
            this.BattleIsReady?.Invoke(sender, args);
        }

        private void ConnectionManager_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Message message = (Message)e.Message;
            switch (message.MessageType)
            {
                case MessageType.NewBattleRequest:
                    this.NewBattle(e.Connection);
                    return;
                case MessageType.JoinBattleRequest:
                    int gameOwner = (int)message.Content;
                    this.JoinBattle(e.Connection, gameOwner);
                    return;
                case MessageType.ConfirmBattle:
                    this.BattleConfirmed(e.Connection);
                    return;
                case MessageType.DeclineBattle:
                    this.BattleDeclined(e.Connection);
                    return;
            }
        }
        private void BattleDeclined(IConnection gameOwner)
        {
            ConnectionArgs args = (ConnectionArgs)gameOwner.ConnectionData;
            IConnection challenger = args.Challenger;
            ConnectionArgs challengerArgs = (ConnectionArgs)challenger.ConnectionData;
            args.Challenger = null;
            challenger.SendMessage(new Message(MessageType.DeclineBattle));
        }

        private void BattleConfirmed(IConnection gameOwner)
        {
            ConnectionArgs args = (ConnectionArgs)gameOwner.ConnectionData;
            IConnection challenger = args.Challenger;
            ConnectionArgs challengerArgs = (ConnectionArgs)challenger.ConnectionData;
            if (args.ClientState != ClientState.WaitingForOpponent || challengerArgs.ClientState != ClientState.Inactive)
            {
                return;
            }

            this.UsersInLobby.Add(challenger);
            this.FireBattleReady(this, new BattleReadyEventArgs(gameOwner, challenger));
        }

        private void NewBattle(IConnection connection)
        {
            ConnectionArgs args = (ConnectionArgs)connection.ConnectionData;

            if (args.ClientState != ClientState.Inactive)
            {
                return;
            }

            args.Id = DateTime.Now.Millisecond;
            args.ClientState = ClientState.WaitingForOpponent;
            connection.ConnectionLost += this.Connection_Disconnected;
            connection.TimedOut += this.Connection_Disconnected;
            this.UsersInLobby.Add(connection);
        }

        private void JoinBattle(IConnection connection, int gameOwnerId)
        {
            ConnectionArgs args = (ConnectionArgs)connection.ConnectionData;
            if (args.ClientState != ClientState.Inactive)
            {
                return;
            }

            try
            {
                IConnection gameOwner = this.UsersInLobby.Where(user => ((ConnectionArgs)user.ConnectionData).Id == gameOwnerId).First();
                if (gameOwner != connection)
                {
                    if (gameOwner.ConnectionData == null)
                    {
                        gameOwner.ConnectionData = new ConnectionArgs();
                    }

                    ConnectionArgs gameOwnerArgs = (ConnectionArgs)gameOwner.ConnectionData;
                    gameOwnerArgs.Challenger = connection;
                    gameOwner.SendMessage(new Message(MessageType.JoinBattleRequest, new ClientDummy(args.UserName, connection.IPAddress, false, args.Id)));
                }
            }
            catch
            {
            }
        }
        private void Connection_Disconnected(object sender, DisconnectedEventArgs e)
        {
            if (this.UsersInLobby.Contains(e.Connection))
            {
                this.UsersInLobby.Remove(e.Connection);
            }
        }
    }
}