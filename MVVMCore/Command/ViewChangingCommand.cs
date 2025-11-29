namespace MorskoyGoy.MVVMCore
{
    public abstract class ViewChangingCommand : BaseCommand
    {
        public ViewChangingCommand(ViewShellBaseViewModel viewShell)
        {
            this.ViewShell = viewShell;
        }

        protected ViewShellBaseViewModel ViewShell
        {
            get;
            set;
        }
    }
}
