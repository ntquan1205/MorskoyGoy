namespace MorskoyGoy.Command
{
    using Model;
    using MVVMCore;
    using ViewModels.ViewRepresenter;
    internal class DeclineChallengeCommand : BaseCommand
    {
        private Client client;

        private LobbyViewModel lobby;
        public DeclineChallengeCommand(Client client, LobbyViewModel lobby)
        {
            this.client = client;
            this.lobby = lobby;
        }
        public override void Execute(object parameter)
        {
            this.client.SendDeclineBattle();
            this.lobby.ModalVisible = false;
        }
    }
}
