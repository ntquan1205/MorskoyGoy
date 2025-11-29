namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.IO;
    using System.Text.Json;

    public static class MessageHandler
    {
        public static void Send(object message, EnhancedTcpClient client)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "The value must not be null.");
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "The value must not be null.");
            }

            string json = JsonSerializer.Serialize(new MessageContainer(message));
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            client.Write(data);
        }

        public static object Read(byte[] data)
        {
            string json = System.Text.Encoding.UTF8.GetString(data);
            MessageContainer container = JsonSerializer.Deserialize<MessageContainer>(json);
            return container.Content;
        }
    }
}