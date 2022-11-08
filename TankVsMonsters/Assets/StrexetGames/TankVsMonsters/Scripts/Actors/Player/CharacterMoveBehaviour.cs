using UnityEngine;
using UsefulTools.Runtime.DataStructures;

namespace _Game_.Scripts.Actors.Player
{
    public class CharacterMoveBehaviour : MonoBehaviourImplementation<IMoveBehaviour>, IMoveBehaviour
    {
        [SerializeField] private CharacterController _characterController;

        private Camera _camera;
        private Vector3 _movementVector;

        public Vector3 CurrentPosition
        {
            get => transform.position;
            set => WarpTo(value);
        }

        private void Start() => _camera = Camera.main;

        public void UpdateMoveInput(Vector2 movementAxis)
        {
            var movementVector = Vector3.zero;

            if (movementAxis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(movementAxis);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            _movementVector = movementVector;
        }

        public void Move(MovementData movementData)
        {
            var movement = _movementVector;

            if (movement.sqrMagnitude > Constants.Epsilon)
            {
                transform.forward = movement;
            }

            movement += UnityEngine.Physics.gravity;
            _characterController.Move(Time.fixedDeltaTime * movementData.ForwardSpeed * movement);
        }

        private void WarpTo(Vector3 position)
        {
            _characterController.enabled = false;
            transform.position = position;
            _characterController.enabled = true;
        }
    }
}