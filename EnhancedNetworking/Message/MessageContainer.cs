
namespace MorskoyGoy.EnhancedNetworking
{
    using System;


    [Serializable]
    public class MessageContainer
    {
 
        public MessageContainer(object content = null)
        {
            this.Content = content;
        }

        public object Content
        {
            get;
            protected set;
        }
    }
}