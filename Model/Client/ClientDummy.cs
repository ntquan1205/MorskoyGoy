
namespace MorskoyGoy.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    public class ClientDummy
    {
        public ClientDummy(string userName, string ipAdress, bool inGame, int id)
        {
            this.UserName = userName;
            this.IPAddress = ipAdress ?? throw new ArgumentNullException("The value must not be null.");
            this.InGame = inGame;
            this.Id = id;
        }

        public string UserName
        {
            get;
            private set;
        }

        public string IPAddress
        {
            get;
            private set;
        }
        public bool InGame
        {
            get;
            private set;
        }
        public int Id
        {
            get;
            set;
        }
    }
}
