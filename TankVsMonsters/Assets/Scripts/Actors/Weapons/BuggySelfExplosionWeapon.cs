using UnityEngine;

namespace Actors.Weapons
{
    public class BuggySelfExplosionWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private ParticleSystem[] _explosions;
        
        
        public void Attack(GameObject attacker, GameObject attacked)
        {
            Debug.Log($"[DEBUG]<color=yellow>{nameof(BuggySelfExplosionWeapon)}.{nameof(Attack)}></color> " +
                      $"BANG! -- {gameObject.name} died in furious explosion!");

            foreach (var explosion in _explosions)
            {
                explosion.transform.SetParent(null);
                explosion.Play(true);
            }
            
            attacker.SetActive(false);
            Destroy(attacker);
        }
    }
}