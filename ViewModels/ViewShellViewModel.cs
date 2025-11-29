namespace MorskoyGoy.ViewModels
{
    using System.Net.Sockets;
    using Model;
    using MVVMCore;

    internal class ViewShellViewModel : ViewShellBaseViewModel
    {

        public ViewShellViewModel(TcpListener tcpListener)
        {
            this.TcpListener = tcpListener;
        }
        public TcpListener TcpListener
        {
            get;
            private set;
        }

        public Server Server
        {
            get;
            set;
        }
        public Client Client
        {
            get;
            set;
        }
    }
}
