namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core.States
{
	public interface IPayloadedState<in TPayload> : IExitableState
	{
		void Enter(TPayload payload);
	}
}