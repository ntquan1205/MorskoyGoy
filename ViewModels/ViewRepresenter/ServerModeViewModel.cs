
namespace MorskoyGoy.ViewModels.ViewRepresenter
{
    using Command.ViewChangingCommand;
    using EnhancedNetworking;
    using Model;
    using MVVMCore;

    internal class ServerModeViewModel : ViewRepresentingViewModel
    {
        private Server server;

        public ServerModeViewModel(ViewShellBaseViewModel viewShell) : base(viewShell)
        {
            ViewShellViewModel shell = (ViewShellViewModel)this.ViewShell;

            TcpConnectionManager tcpConnectionManager = new TcpConnectionManager(shell.TcpListener, 1600);
            this.server = new Server(tcpConnectionManager);
            shell.Server = this.server;
            this.server.Start();
        }
        public QuitServerModeCommand QuitServerModeCommand => new QuitServerModeCommand(this.ViewShell, this.server);
    }
}
