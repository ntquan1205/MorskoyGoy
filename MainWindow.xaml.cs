namespace MorskoyGoy
{
    using System.ComponentModel;
    using System.Windows;
    using MorskoyGoy.ViewModels;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (this.DataContext is MainWindowViewModel mainWindowViewModel)
            {
                if (mainWindowViewModel.ViewShell.Client != null)
                {
                    mainWindowViewModel.ViewShell.Client.Close();
                }

                if (mainWindowViewModel.ViewShell.Server != null)
                {
                    mainWindowViewModel.ViewShell.Server.Shutdown();
                }
            }
        }
    }
}