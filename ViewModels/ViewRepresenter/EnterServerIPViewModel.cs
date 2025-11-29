namespace MorskoyGoy.ViewModels.ViewRepresenter
{
    using MorskoyGoy.EnhancedNetworking;
    using MorskoyGoy.Model;
    using Command.ViewChangingCommand;
    using MVVMCore;

    internal class EnterServerIPViewModel : ViewRepresentingViewModel
    {
        public EnterServerIPViewModel(ViewShellBaseViewModel viewShell) : base(viewShell)
        {
            ViewShellViewModel shell = (ViewShellViewModel)viewShell;
            shell.Client = new Client(new TcpConnection());
        }

        public ConnectToServerCommand ConnectToServerCommand => new ConnectToServerCommand(this.ViewShell);
    }
}
