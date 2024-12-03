using UnityEngine;
using UsefulTools.Runtime.Pools;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.Weapons
{
	public class SelfExplosionWeapon : MonoBehaviour, IWeapon
	{
		[SerializeField] private Rigidbody _selfRigidbody;
		[SerializeField] private float _maxDamage = 25;

		[Space]
		[SerializeField] private float _explosionRadius;
		[SerializeField] private float _explosionForce;
		[SerializeField] private LayerMask _explosionLayerMask;

		[Space]
		[SerializeField] private bool _destroyOnAttack;

		[Space]
		[SerializeField] private ParticleSystem[] _explosionParticles;

		private readonly static ArrayPool<Collider> CollidersPool = new(5);

		public void Attack(GameObject attacker, GameObject attacked)
		{
			using var colliders = CollidersPool.GetArray();
			var explosionPosition = transform.position;
			var overlapCount = UnityEngine.Physics.OverlapSphereNonAlloc(explosionPosition, _explosionRadius, colliders.RawData, _explosionLayerMask);

			AddExplosionForce(explosionPosition, overlapCount, colliders.RawData);
			ApplyExplosionDamage(explosionPosition, overlapCount, colliders.RawData);

			PlayParticles();

			if (_destroyOnAttack)
			{
				DestroyAttacker(attacker);
			}
		}

		private void AddExplosionForce(Vector3 explosionPosition, int overlapCount, Collider[] colliders)
		{
			for (var i = 0; i < overlapCount; i++)
			{
				if (colliders[i].TryGetComponent<Rigidbody>(out var rb) && rb != _selfRigidbody)
				{
					rb.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
				}
			}
		}

		private void ApplyExplosionDamage(Vector3 explosionPosition, int overlapCount, Collider[] colliders)
		{
			// for (var i = 0; i < overlapCount; i++)
			// {
			// 	var targetCollider = colliders[i];
			//
			// 	if (targetCollider.TryGetComponent<IHealth>(out var health) && rb != _selfRigidbody)
			// 	{
			// 		var targetPosition = targetCollider.transform.position;
			// 		var distanceVector = explosionPosition - targetPosition;
			// 		var distanceSqr = distanceVector.sqrMagnitude;
			//
			// 		var damage = _maxDamage / distanceSqr;
			// 		health.TakeDamage(damage);
			// 	}
			// }
		}

		private void PlayParticles()
		{
			foreach (var explosion in _explosionParticles)
			{
				explosion.transform.SetParent(null);
				explosion.transform.SetPositionAndRotation(transform.position, transform.rotation);
				explosion.Play(true);
			}
		}

		private void DestroyAttacker(GameObject attacker)
		{
			attacker.SetActive(false);
			Destroy(attacker);
			Debug.Log($"[DEBUG]<color=yellow>{nameof(SelfExplosionWeapon)}.{nameof(Attack)}></color> " + $"BANG! -- {gameObject.name} died in furious explosion!");
		}
	}
}