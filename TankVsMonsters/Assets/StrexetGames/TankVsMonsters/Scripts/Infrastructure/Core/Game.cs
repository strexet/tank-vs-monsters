using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core.States;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.GameStates;
using StrexetGames.TankVsMonsters.Scripts.UI;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core
{
	public class Game
	{
		public StateMachine StateMachine { get; }

		public Game(LoadingCurtain loadingCurtain) =>
			StateMachine = GameStateMachine.Create(new SceneLoader(), loadingCurtain, ServiceLocator.Container);
	}
}