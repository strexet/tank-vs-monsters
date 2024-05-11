using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : IAttackerInputService
    {
        bool IsNextWeaponButtonPressed { get; }
        bool IsPreviousWeaponButtonPressed { get; }
        bool IsJumpButtonPressed { get; }
    }

    public interface IAttackerInputService : IService
    {
        Vector2 MovementAxis { get; }
        bool IsAttackButtonPressed { get; }
    }
}