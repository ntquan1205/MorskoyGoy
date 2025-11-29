namespace MorskoyGoy.ViewModels
{
    using MVVMCore;

    internal class BattleFieldModalViewModel : NotifyingViewModel
    {

        private BaseCommand modalCommand;

        private BaseCommand abortCommand;

        private string text;

        private bool buttonSetYesNo;

        private bool buttonSetOK;

        public BattleFieldModalViewModel(string text)
        {
            this.Text = text;
        }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.Notify();
            }
        }
        public bool ButtonSetYesNo
        {
            get
            {
                return this.buttonSetYesNo;
            }

            set
            {
                this.buttonSetYesNo = value;
                this.Notify();
            }
        }
        public bool ButtonSetOK
        {
            get
            {
                return this.buttonSetOK;
            }

            set
            {
                this.buttonSetOK = value;
                this.Notify();
            }
        }
        public BaseCommand ModalCommand
        {
            get
            {
                return this.modalCommand;
            }

            set
            {
                this.modalCommand = value;
                this.Notify();
            }
        }

        public BaseCommand AbortCommand
        {
            get
            {
                return this.abortCommand;
            }

            set
            {
                this.abortCommand = value;
                this.Notify();
            }
        }
    }
}