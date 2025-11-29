namespace MorskoyGoy.MVVMCore
{
    public abstract class ViewRepresentingViewModel : NotifyingViewModel
    {
        public ViewRepresentingViewModel(ViewShellBaseViewModel viewShell)
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
