namespace MorskoyGoy.Model
{
    using System;
    using EnhancedNetworking;

    internal class SendAliveMessageTask : NetworkTask
    {

        public SendAliveMessageTask(IConnection connection, int intervalTime = 1000) : base(connection, intervalTime)
        {
            this.Connection = connection ?? throw new ArgumentNullException(nameof(connection), "The value must not be null.");
        }

        protected override void Execute(object parameter)
        {
            this.Connection.SendMessage(new Message(MessageType.AliveMessage));
        }
    }
}
