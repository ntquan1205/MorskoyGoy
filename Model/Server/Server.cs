
namespace MorskoyGoy.Model
{
    using System;
    using System.Collections.Generic;
    using EnhancedNetworking;

    public class Server
    {
        private BattleManager battleManager;

        private MatchMaker matchMaker;

        private IConnectionManager connectionManager;

        public Server(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager), "The value must not be null.");
            this.connectionManager.ConnectionAccepted += this.ConnectionManager_ConnectionAccepted;
            this.connectionManager.MessageReceived += this.ConnectionManager_MessageReceived;
            this.matchMaker = new MatchMaker(this.connectionManager);
            this.matchMaker.BattleIsReady += this.MatchMaker_BattleIsReady;
            this.battleManager = new BattleManager(this.matchMaker.UsersInLobby);
        }

        public void Start()
        {
            this.connectionManager.Start();
        }

        public void Shutdown()
        {
            this.connectionManager.Stop();
        }

        private void ConnectionManager_ConnectionAccepted(object sender, ClientAcceptedEventArgs args)
        {
            ConnectionArgs connectionData = new ConnectionArgs();

            // TODO: Implement user name.
            connectionData.UserName = "John Doe";
            connectionData.ClientState = ClientState.Inactive;
            IConnection connection = args.Connection;
            connection.ConnectionData = connectionData;
        }

        private void MatchMaker_BattleIsReady(object sender, BattleReadyEventArgs e)
        {
            this.battleManager.InitiateBattle(e.CompetitorA, e.CompetitorB);
        }

        private void ConnectionManager_MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            Message message = (Message)args.Message;
            IConnection connection = (IConnection)args.Connection;

            switch (message.MessageType)
            {
                case MessageType.AliveMessage:
                    return;
                case MessageType.LobbyRequest:
                    this.Connection_LobbyRequestReceived(message, connection);
                    return;
            }
        }
        private void Connection_LobbyRequestReceived(Message message, IConnection connection)
        {
            List<ClientDummy> lobby = this.matchMaker.Lobby;
            connection.SendMessage(new Message(MessageType.LobbyResponse, lobby));
        }
    }
}