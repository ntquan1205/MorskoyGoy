
namespace MorskoyGoy.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using EnhancedNetworking;

    public class Competitor
    {

        public Competitor(IConnection connection)
        {
            this.Connection = connection ?? throw new ArgumentNullException(nameof(connection), "The value must not be null.");
            this.Connection.MessageReceived += this.Connection_MoveReceived;
            this.Connection.ConnectionLost += this.FireLeftGame;
            this.Connection.TimedOut += this.FireLeftGame;
            this.BattleField = new BattleField();
        }

        public event EventHandler<ShipPositionsReceivedEventArgs> ShipPositionsReceived;

        public event EventHandler<MoveReceivedEventArgs> MoveReceived;

        public event EventHandler<EventArgs> LeftGame;
        public BattleField BattleField
        {
            get;
            private set;
        }

        public IConnection Connection
        {
            get;
            private set;
        }

        public bool PositionsReady
        {
            get;
            set;
        }
        public void SendInitiateBattle()
        {
            this.Connection.SendMessage(new Message(MessageType.InitiateBattle));
        }

        public void SendMoveRequest()
        {
            this.Connection.SendMessage(new Message(MessageType.MoveRequest));
        }

        public void SendOpponentsMove(Marker marker)
        {
            if (marker == null)
            {
                throw new ArgumentNullException(nameof(marker), "The value must not be null.");
            }

            this.Connection.SendMessage(new Message(MessageType.OpponentsMove, marker));
        }

        public void SendMoveReport(Marker marker)
        {
            if (marker == null)
            {
                throw new ArgumentNullException(nameof(marker), "The value must not be null.");
            }

            this.Connection.SendMessage(new Message(MessageType.MoveReport, marker));
        }

        public void SendGameLost()
        {
            this.Connection.SendMessage(new Message(MessageType.GameLost));
        }

        public void SendGameWon()
        {
            this.Connection.SendMessage(new Message(MessageType.GameWon));
        }

        protected virtual void FireShipPositionsReceived(object sender, ShipPositionsReceivedEventArgs args)
        {
            if (this.ShipPositionsReceived != null)
            {
                this.ShipPositionsReceived(sender, args);
            }
        }
        protected virtual void FireMoveReceived(object sender, MoveReceivedEventArgs args)
        {
            if (this.MoveReceived != null)
            {
                this.MoveReceived(sender, args);
            }
        }

        protected virtual void FireLeftGame(object sender, EventArgs e)
        {
            this.LeftGame?.Invoke(this, e);
        }
        private void Connection_MoveReceived(object sender, MessageReceivedEventArgs e)
        {
            Message message = (Message)e.Message;

            if (message.MessageType == MessageType.MoveResponse)
            {
                Position position = (Position)message.Content;
                this.FireMoveReceived(this, new MoveReceivedEventArgs(position));
                return;
            }

            if (message.MessageType == MessageType.ShipPositions)
            {
                List<Ship> positions = (List<Ship>)message.Content;
                this.FireShipPositionsReceived(this, new ShipPositionsReceivedEventArgs(positions));
            }
        }
    }
}