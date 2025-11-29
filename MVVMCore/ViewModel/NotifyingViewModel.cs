namespace MorskoyGoy.MVVMCore
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    public abstract class NotifyingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void Notify([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
