using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Factory;
using Unity.Cinemachine;
using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.CameraLogic
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] private CinemachineCamera _virtualCamera;
		private IGameFactory _gameFactory;

		private void Awake()
		{
			_gameFactory = ServiceLocator.Container.Single<IGameFactory>();

			if (_gameFactory.IsPlayerCreated)
			{
				SetCameraTarget();
			}
			else
			{
				_gameFactory.PlayerCreated += SetCameraTarget;
			}
		}

		private void SetCameraTarget()
		{
			_gameFactory.PlayerCreated -= SetCameraTarget;
			_virtualCamera.Follow = _gameFactory.PlayerTransform;
		}
	}
}