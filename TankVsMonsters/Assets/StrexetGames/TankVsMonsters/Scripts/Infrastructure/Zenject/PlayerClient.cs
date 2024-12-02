using StrexetGames.TankVsMonsters.Scripts.Actors.Player;
using UnityEngine;
using Zenject;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Zenject
{
	public class PlayerClient : MonoBehaviour
	{
		private PlayerMove _playerMove;

		[Inject]
		private void Construct(PlayerMove playerMove) => _playerMove = playerMove;
	}
}