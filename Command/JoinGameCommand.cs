namespace MorskoyGoy.Command
{
    using Model;
    using MVVMCore;
    internal class JoinGameCommand : BaseCommand
    {
        private Client client;
        public JoinGameCommand(Client client)
        {
            this.client = client;
        }
        public override void Execute(object parameter)
        {
            int id = (int)parameter;
            this.client.SendJoinBattleRequest(id);
        }
    }
}
