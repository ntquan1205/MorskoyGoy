
namespace MorskoyGoy.ViewModels
{
    using Command;
    using Model;
    internal class BattleListEntryViewModel
    {

        private Client client;
        public BattleListEntryViewModel(string userName, string ipAddress, bool inGame, int id, Client client)
        {
            this.UserName = userName;
            this.IPAddress = ipAddress;
            this.InGame = inGame;
            this.Id = id;
            this.client = client;
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

        public int Id
        {
            get;
            private set;
        }

        public bool InGame
        {
            get;
            private set;
        }
        public JoinGameCommand JoinGameCommand => new JoinGameCommand(this.client);
    }
}