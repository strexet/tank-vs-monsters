using UnityEngine;

namespace Actors.Player
{
    public interface IPlayerMoveBehaviour
    {
        Vector3 CurrentPosition { get; set; }

        void UpdateMoveInput(Vector2 movementAxis);
        void Move(MovementData movementData);
    }
}