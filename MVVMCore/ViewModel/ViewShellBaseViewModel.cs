
namespace MorskoyGoy.MVVMCore
{
    public class ViewShellBaseViewModel : NotifyingViewModel
    {
        private ViewRepresentingViewModel view;
        public ViewRepresentingViewModel View
        {
            get
            {
                return this.view;
            }

            set
            {
                this.view = value;
                this.Notify();
            }
        }
    }
}
