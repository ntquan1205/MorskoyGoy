
namespace MorskoyGoy.ViewModels
{
    using System.Net;
    using System.Net.Sockets;
    using ViewRepresenter;
    internal class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            this.TcpListener = new TcpListener(IPAddress.Any, 1337);
            this.ViewShell = new ViewShellViewModel(this.TcpListener);
            this.ViewShell.View = new MainMenuViewModel(this.ViewShell);
        }

        public TcpListener TcpListener
        {
            get;
            private set;
        }

        public ViewShellViewModel ViewShell
        {
            get;
            private set;
        }
    }
}
