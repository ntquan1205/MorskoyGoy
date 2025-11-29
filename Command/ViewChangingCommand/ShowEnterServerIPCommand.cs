namespace MorskoyGoy.Command.ViewChangingCommand
{
    using MVVMCore;
    using ViewModels;
    using ViewModels.ViewRepresenter;

    internal class ShowEnterServerIPCommand : ViewChangingCommand
    {
        public ShowEnterServerIPCommand(ViewShellBaseViewModel viewShell) : base(viewShell)
        {
        }
        public override void Execute(object parameter)
        {
            this.ViewShell.View = new EnterServerIPViewModel((ViewShellViewModel)this.ViewShell);
        }
    }
}
