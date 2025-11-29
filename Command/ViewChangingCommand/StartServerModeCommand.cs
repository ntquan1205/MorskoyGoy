namespace MorskoyGoy.Command.ViewChangingCommand
{
    using System.Windows;
    using MVVMCore;
    using ViewModels.ViewRepresenter;

    internal class StartServerModeCommand : ViewChangingCommand
    {
        public StartServerModeCommand(ViewShellBaseViewModel viewShell) : base(viewShell)
        {
        }
        public override void Execute(object parameter)
        {
            try
            {
                this.ViewShell.View = new ServerModeViewModel(this.ViewShell);
            }
            catch
            {
                MessageBox.Show("Couldn't create a server. Be aware that you can only have one server per machine running.", "Error", MessageBoxButton.OK);
                this.ViewShell.View = new MainMenuViewModel(this.ViewShell);
            }
        }
    }
}
