namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.GameStates
{
    public interface IPayloadedState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}