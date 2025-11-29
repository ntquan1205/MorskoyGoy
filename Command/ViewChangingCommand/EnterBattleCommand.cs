namespace MorskoyGoy.Command.ViewChangingCommand
{
    using MVVMCore;
    using ViewModels.ViewRepresenter;
    internal class EnterBattleCommand : ViewChangingCommand
    {
        public EnterBattleCommand(ViewShellBaseViewModel viewShell) : base(viewShell)
        {
        }
        public override void Execute(object parameter)
        {
            this.ViewShell.View = new BattleFieldViewModel(this.ViewShell);
        }
    }
}
