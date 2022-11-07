using UnityEngine;

namespace Actors.Weapons
{
    public interface IWeapon
    {
        void Attack(GameObject attacker, GameObject attacked);
    }
}