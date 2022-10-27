using UnityEngine;

namespace Actors.Player
{
    public interface IPlayerMoveBehaviour
    {
        void UpdateMoveInput(Vector2 movementAxis);
        void Move(MovementData movementData);
    }
}