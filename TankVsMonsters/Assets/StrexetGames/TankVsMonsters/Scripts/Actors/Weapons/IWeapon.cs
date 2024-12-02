using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.Weapons
{
	public interface IWeapon
	{
		void Attack(GameObject attacker, GameObject attacked);
	}
}