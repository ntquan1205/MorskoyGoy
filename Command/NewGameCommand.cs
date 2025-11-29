namespace MorskoyGoy.Command
{
    using Model;
    using MVVMCore;

    internal class NewGameCommand : BaseCommand
    {
        private Client client;

        public NewGameCommand(Client client)
        {
            this.client = client;
        }
        public override void Execute(object parameter)
        {
            this.client.SendNewBattleRequest();
        }
    }
}
