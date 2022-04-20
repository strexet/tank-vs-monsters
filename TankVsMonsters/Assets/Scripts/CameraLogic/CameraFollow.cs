using Cinemachine;
using Infrastructure.Services;
using Infrastructure.Services.Factory;
using UnityEngine;

namespace GameCamera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
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