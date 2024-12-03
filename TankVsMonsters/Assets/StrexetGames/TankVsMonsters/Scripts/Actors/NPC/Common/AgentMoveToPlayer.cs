using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.NPC.Common
{
	public class AgentMoveToPlayer : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent _navMeshAgent;
		[SerializeField] private float _maxDistance = 20.0f;
		[SerializeField] private float _minDistance = 0.5f;

		private IGameFactory _gameFactory;

		private void Awake() => _gameFactory = ServiceLocator.Container.Single<IGameFactory>();

		private void Update()
		{
			if (!_gameFactory.IsPlayerCreated)
			{
				return;
			}
			
			var distanceSqr = GetPlayerDistanceSqr();

			if (IsPlayerTooFar(distanceSqr)
			    || PlayerReached(distanceSqr))
			{
				return;
			}

			_navMeshAgent.destination = _gameFactory.PlayerTransformData.position;
		}

		private float GetPlayerDistanceSqr()
		{
			var playerPosition = _gameFactory.PlayerTransformData.position;
			var distanceVector = playerPosition - transform.position;
			return distanceVector.sqrMagnitude;
		}

		private bool IsPlayerTooFar(float sqrDistance) => sqrDistance > _maxDistance * _maxDistance;

		private bool PlayerReached(float sqrDistance) => sqrDistance <= _minDistance * _minDistance;

		private void OnDrawGizmosSelected()
		{
			var prevColor = Gizmos.color;
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _minDistance);
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, _maxDistance);
			Gizmos.color = prevColor;
		}
	}
}