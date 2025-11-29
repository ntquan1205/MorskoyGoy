namespace MorskoyGoy.Model
{
    using System;

    [Serializable]
    public class Message
    {

        public Message(MessageType messageType, object content = null)
        {
            this.MessageType = messageType;
            this.Content = content;
        }


        public MessageType MessageType
        {
            get;
            private set;
        }
        public object Content
        {
            get;
            private set;
        }
    }
}