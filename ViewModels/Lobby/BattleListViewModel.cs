namespace MorskoyGoy.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;
    using MorskoyGoy.Model;
    using MVVMCore;


    internal class BattleListViewModel : NotifyingViewModel
    {
        private Client client;

        private ObservableCollection<BattleListEntryViewModel> battleList;

        public BattleListViewModel(Client client)
        {
            this.BattleList = new ObservableCollection<BattleListEntryViewModel>();
            this.client = client;
            this.client.LobbyReceived += this.Client_LobbyReceived;
            this.client.StartSendingLobbyRequests();
        }


        public ObservableCollection<BattleListEntryViewModel> BattleList
        {
            get
            {
                return this.battleList;
            }

            private set
            {
                this.battleList = value;
                this.Notify();
            }
        }
        private void Client_LobbyReceived(object sender, LobbyReceivedEventArgs e)
        {
            this.BattleList = new ObservableCollection<BattleListEntryViewModel>();

            for (int i = 0; i < e.ClientDummies.Count; i++)
            {
                ClientDummy client = e.ClientDummies[i];
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.BattleList.Add(new BattleListEntryViewModel(client.UserName, client.IPAddress, client.InGame, client.Id, this.client));
                });
            }
        }
    }
}
