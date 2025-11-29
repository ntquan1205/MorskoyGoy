namespace MorskoyGoy.Command.ViewChangingCommand
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Model;
    using MVVMCore;
    using ViewModels;
    using ViewModels.ViewRepresenter;
    internal class ConnectToServerCommand : ViewChangingCommand
    {
        public ConnectToServerCommand(ViewShellBaseViewModel viewShell) : base(viewShell)
        {
        }
        public override void Execute(object parameter)
        {
            ViewShellViewModel shell = (ViewShellViewModel)this.ViewShell;
            string ip = (string)parameter;

            try
            {
                shell.Client.ServerIP = IPAddress.Parse(ip);
                shell.Client.Connect();
            }
            catch
            {
                MessageBox.Show("The entered IP Address is either invalid or the server is down!", "Failed to connect.", MessageBoxButton.OK);
                shell.View = new MainMenuViewModel(shell);
                return;
            }

            shell.View = new LobbyViewModel(shell);
        }
    }
}
