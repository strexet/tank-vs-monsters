using UnityEngine;

namespace _Game_.Scripts.Actors.Weapons
{
    public interface IWeapon
    {
        void Attack(GameObject attacker, GameObject attacked);
    }
}