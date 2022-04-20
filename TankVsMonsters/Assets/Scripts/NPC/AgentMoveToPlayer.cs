using Infrastructure.Services;
using Infrastructure.Services.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _minDistance = 0.5f;

        private IGameFactory _gameFactory;

        private void Awake() => _gameFactory = ServiceLocator.Container.Single<IGameFactory>();

        private void Update()
        {
            if (!_gameFactory.IsPlayerCreated || PlayerReached())
            {
                return;
            }

            _navMeshAgent.destination = _gameFactory.PlayerTransformData.position;
        }

        private bool PlayerReached()
        {
            var playerPosition = _gameFactory.PlayerTransformData.position;
            var distanceVector = playerPosition - transform.position;
            return distanceVector.sqrMagnitude <= _minDistance * _minDistance;
        }
    }
}