namespace MorskoyGoy.Command
{
    using MVVMCore;
    using ViewModels.ViewRepresenter;
    internal class CloseLobbyModalCommand : BaseCommand
    {
        private LobbyViewModel lobby;
        public CloseLobbyModalCommand(LobbyViewModel lobby)
        {
            this.lobby = lobby;
        }
        public override void Execute(object parameter)
        {
            this.lobby.ModalVisible = false;
        }
    }
}
