
namespace MorskoyGoy.Model
{


    public enum MessageType
    {
 
        AliveMessage,

        MoveRequest,

        MoveResponse,

        OpponentsMove,

        MoveReport,

        ShipPositions,

        LobbyResponse,

        LobbyRequest,

        NewBattleRequest,

        JoinBattleRequest,

        ConfirmBattle,

        DeclineBattle,

        InitiateBattle,

        GameWon,

        GameLost
    }
}