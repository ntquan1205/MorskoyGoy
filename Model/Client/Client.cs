
namespace MorskoyGoy.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using EnhancedNetworking;

    public class Client
    {

        private IConnection connection;

        private NetworkTask sendLobbyRequestTask;

        private NetworkTask sendAliveMessageTask;

        public Client(IConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException("The value must not be null.");
            this.connection.MessageReceived += this.Connection_MessageReceived;
            this.connection.ConnectionFailed += this.FireConnectionFailed;
            this.connection.ConnectionLost += this.FireConnectionLost;
        }

        public event EventHandler<EventArgs> ConnectionLost;

        public event EventHandler<EventArgs> ConnectionFailed;

        public event EventHandler<MoveReceivedEventArgs> OpponentMoveReceived;

        public event EventHandler<MoveReceivedEventArgs> MoveReportReceived;

        public event EventHandler<EventArgs> MoveRequestReceived;

        public event EventHandler<LobbyReceivedEventArgs> LobbyReceived;

        public event EventHandler<EventArgs> InitiateBattleReceived;

        public event EventHandler<EventArgs> GameWon;

        public event EventHandler<EventArgs> GameLost;

        public event EventHandler<ChallengerFoundEventArgs> ChallengerFound;

        public event EventHandler<EventArgs> OpponentDeclined;

        public BattleField BattleField
        {
            get;
            private set;
        }

        public IPAddress ServerIP
        {
            get;
            set;
        }

        public void SendMove(Position position)
        {
            this.connection.SendMessage(new Message(MessageType.MoveResponse, position));
        }

        public void SendShipPositions(List<Ship> ships)
        {
            this.connection.SendMessage(new Message(MessageType.ShipPositions, ships));
        }

        public void SendNewBattleRequest()
        {
            this.connection.SendMessage(new Message(MessageType.NewBattleRequest));
        }

        public void SendJoinBattleRequest(int id)
        {
            this.connection.SendMessage(new Message(MessageType.JoinBattleRequest, id));
        }

        public void SendConfirmBattle()
        {
            this.connection.SendMessage(new Message(MessageType.ConfirmBattle));
        }

        public void SendDeclineBattle()
        {
            this.connection.SendMessage(new Message(MessageType.DeclineBattle));
        }

        public void Connect()
        {
            if (this.ServerIP == null)
            {
                throw new ArgumentNullException(nameof(this.ServerIP), "The value must not be null");
            }

            this.connection.Connect(new IPEndPoint(this.ServerIP, 1337));

            if (!this.connection.Connected)
            {
                this.FireConnectionFailed(this, EventArgs.Empty);
                return;
            }

            this.sendAliveMessageTask = new SendAliveMessageTask(this.connection);
            this.sendAliveMessageTask.Start(null, true);
            this.connection.StartListening();
        }

        public void Close()
        {
            if (this.connection != null)
            {
                this.connection.Close();
            }

            if (this.sendAliveMessageTask != null)
            {
                this.sendAliveMessageTask.Stop();
            }

            if (this.sendLobbyRequestTask != null)
            {
                this.sendLobbyRequestTask.Stop();
            }

            this.ConnectionLost = null;
        }

        public void StartSendingLobbyRequests()
        {
            this.sendLobbyRequestTask = new SendLobbyRequestTask(this.connection);
            this.sendLobbyRequestTask.Start(null, true);
        }

        public void StopSendingLobbyRequests()
        {
            this.sendLobbyRequestTask.Stop();
        }

        protected virtual void FireLobbyReceived(object sender, LobbyReceivedEventArgs args)
        {
            this.LobbyReceived?.Invoke(sender, args);
        }

        protected virtual void FireOpponentMoveReceived(object sender, MoveReceivedEventArgs args)
        {
            this.OpponentMoveReceived?.Invoke(sender, args);
        }

        protected virtual void FireMoveRequestReceived(object sender, EventArgs args)
        {
            this.MoveRequestReceived?.Invoke(sender, args);
        }
        protected virtual void FireInitiateBattleReceived(object sender, EventArgs args)
        {
            this.InitiateBattleReceived?.Invoke(sender, args);
        }

        protected virtual void FireMoverReportReceived(object sender, MoveReceivedEventArgs args)
        {
            this.MoveReportReceived?.Invoke(sender, args);
        }
        protected virtual void FireGameWon(object sender, EventArgs empty)
        {
            this.GameWon?.Invoke(sender, empty);
        }
        protected virtual void FireGameLost(object sender, EventArgs empty)
        {
            this.GameLost?.Invoke(sender, empty);
        }
        protected virtual void FireConnectionFailed(object sender, EventArgs args)
        {
            this.ConnectionFailed?.Invoke(sender, args);
        }

        protected virtual void FireConnectionLost(object sender, DisconnectedEventArgs e)
        {
            this.ConnectionLost?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void FireChallengerFound(object sender, ChallengerFoundEventArgs e)
        {
            this.ChallengerFound?.Invoke(sender, e);
        }

        protected virtual void FireOpponentDeclined(object sender, EventArgs empty)
        {
            this.OpponentDeclined?.Invoke(sender, empty);
        }
        private void Connection_MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            Message message = (Message)args.Message;

            switch (message.MessageType)
            {
                case MessageType.LobbyResponse:
                    this.FireLobbyReceived(this, new LobbyReceivedEventArgs((List<ClientDummy>)message.Content));
                    return;
                case MessageType.OpponentsMove:
                    this.FireOpponentMoveReceived(sender, new MoveReceivedEventArgs((Marker)message.Content));
                    return;
                case MessageType.MoveRequest:
                    this.FireMoveRequestReceived(sender, EventArgs.Empty);
                    return;
                case MessageType.JoinBattleRequest:
                    this.FireChallengerFound(sender, new ChallengerFoundEventArgs((ClientDummy)message.Content));
                    return;
                case MessageType.DeclineBattle:
                    this.FireOpponentDeclined(sender, EventArgs.Empty);
                    return;
                case MessageType.InitiateBattle:
                    this.FireInitiateBattleReceived(sender, EventArgs.Empty);
                    return;
                case MessageType.MoveReport:
                    this.FireMoverReportReceived(sender, new MoveReceivedEventArgs((Marker)message.Content));
                    return;
                case MessageType.GameLost:
                    this.FireGameLost(sender, EventArgs.Empty);
                    return;
                case MessageType.GameWon:
                    this.FireGameWon(sender, EventArgs.Empty);
                    return;
            }
        }
    }
}