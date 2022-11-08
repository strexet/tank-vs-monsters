using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MovementAxis { get; }
        bool IsAttackButtonPressed { get; }
        bool IsNextWeaponButtonPressed { get; }
        bool IsPreviousWeaponButtonPressed { get; }
    }
}