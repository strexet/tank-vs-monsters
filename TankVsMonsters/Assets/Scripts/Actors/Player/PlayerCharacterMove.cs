using Infrastructure.Services;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Actors.Player
{
    public class PlayerCharacterMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private MovementData _movementData;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake() => _inputService = ServiceLocator.Container.Single<IInputService>();

        private void Start() => _camera = Camera.main;

        private void FixedUpdate()
        {
            var movementVector = Vector3.zero;

            if (_inputService.MovementAxis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.MovementAxis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            Move(movementVector);
        }

        private void Move(Vector3 movementVector)
        {
            movementVector += UnityEngine.Physics.gravity;
            _characterController.Move(Time.fixedDeltaTime * _movementData.ForwardSpeed * movementVector);
        }
    }
}