using Infrastructure.DataTypes;
using UnityEngine;

namespace Actors.Weapons
{
    public class SelfExplosionWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Rigidbody _selfRigidbody;

        [Space]
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionForce;
        [SerializeField] private LayerMask _explosionLayerMask;

        [Space]
        [SerializeField] private bool _destroyOnAttack;

        [Space]
        [SerializeField] private ParticleSystem[] _explosions;

        private static readonly ArrayPool<Collider> CollidersPool = new(5);

        public void Attack(GameObject attacker, GameObject attacked)
        {
            AddExplosionForce();
            PlayParticles();
            RemoveAttacker(attacker);
        }

        private void AddExplosionForce()
        {
            var explosionPosition = transform.position;
            var colliders = CollidersPool.GetArray();
            var overlapCount = UnityEngine.Physics.OverlapSphereNonAlloc(explosionPosition,
                _explosionRadius, colliders, _explosionLayerMask);

            for (var i = 0; i < overlapCount; i++)
            {
                var rb = colliders[i].GetComponent<Rigidbody>();

                if (rb != null && rb != _selfRigidbody)
                {
                    rb.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
                }
            }

            CollidersPool.Return(colliders);
        }

        private void PlayParticles()
        {
            foreach (var explosion in _explosions)
            {
                explosion.transform.SetParent(null);
                explosion.transform.SetPositionAndRotation(transform.position, transform.rotation);
                explosion.Play(true);
            }
        }

        private void RemoveAttacker(GameObject attacker)
        {
            if (_destroyOnAttack)
            {
                attacker.SetActive(false);
                Destroy(attacker);
                Debug.Log($"[DEBUG]<color=yellow>{nameof(SelfExplosionWeapon)}.{nameof(Attack)}></color> " +
                          $"BANG! -- {gameObject.name} died in furious explosion!");
            }
        }
    }
}