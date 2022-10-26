using UnityEngine;

namespace Actors.Player
{
    public interface IMoveBehaviour
    {
        void UpdateMoveInput(Vector2 movementAxis);
        void Move(MovementData movementData);
    }
}