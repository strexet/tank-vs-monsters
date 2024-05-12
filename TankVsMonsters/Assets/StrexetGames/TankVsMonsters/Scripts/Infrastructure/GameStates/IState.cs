namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.GameStates
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}