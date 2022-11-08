using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MovementAxis { get; }
        bool IsAttackButtonPressed { get; }
        bool IsNextWeaponButtonPressed { get; }
        bool IsPreviousWeaponButtonPressed { get; }
    }
}