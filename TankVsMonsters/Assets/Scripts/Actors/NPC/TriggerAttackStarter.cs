using Actors.Weapons;
using Physics;
using UnityEngine;

namespace Actors.NPC
{
    [RequireComponent(typeof(IWeapon))]
    public class TriggerAttackStarter : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;

        private IWeapon _weapon;

        private void Awake() => _weapon = GetComponent<IWeapon>();

        private void OnEnable() => _triggerObserver.TriggerEnter += OnEnter;
        private void OnDisable() => _triggerObserver.TriggerEnter -= OnEnter;

        private void OnEnter(Collider other) => _weapon.Attack(gameObject, other.gameObject);
    }
}