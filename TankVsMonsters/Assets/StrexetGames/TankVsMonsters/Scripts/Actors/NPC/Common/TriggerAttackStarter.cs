using StrexetGames.TankVsMonsters.Scripts.Actors.Weapons;
using StrexetGames.TankVsMonsters.Scripts.Physics;
using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.NPC.Common
{
	[RequireComponent(typeof(IWeapon))]
	public class TriggerAttackStarter : MonoBehaviour
	{
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private GameObject _owner;

		private IWeapon _weapon;

		private void Awake() => _weapon = GetComponent<IWeapon>();

		private void OnEnable() => _triggerObserver.TriggerEnter += OnEnter;

		private void OnDisable() => _triggerObserver.TriggerEnter -= OnEnter;

		private void OnEnter(Collider other) => _weapon.Attack(_owner, other.gameObject);
	}
}