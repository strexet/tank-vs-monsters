using UnityEngine;
using UsefulTools.Runtime.DataStructures;

namespace Actors.Player
{
    public class PlayerBodyMove : MonoBehaviourImplementation<IMoveBehaviour>, IMoveBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private Vector2 _movementAxis;

        public void UpdateMoveInput(Vector2 movementAxis) => _movementAxis = movementAxis;

        public void Move(MovementData movementData)
        {
            var currentVelocity = movementData.ForwardSpeed * _movementAxis.y;
            var currentAngularVelocity = movementData.RotationSpeed * _movementAxis.x;

            var selfTransform = transform;

            _rigidbody.velocity = currentVelocity * selfTransform.forward;
            _rigidbody.angularVelocity = currentAngularVelocity * selfTransform.up;
        }
    }
}