using Infrastructure.Services;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Actors.Player
{
    public class PlayerBodyMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MovementData _movementData;

        private IInputService _inputService;

        private float _currentVelocity;
        private float _currentAngularVelocity;

        private void Awake() => _inputService = ServiceLocator.Container.Single<IInputService>();

        private void Update() => UpdateVelocitiesFromInput();

        private void FixedUpdate() => Move();

        private void UpdateVelocitiesFromInput()
        {
            var input = _inputService.MovementAxis;
            _currentVelocity = _movementData.ForwardSpeed * input.y;
            _currentAngularVelocity = _movementData.RotationSpeed * input.x;
        }

        private void Move()
        {
            var selfTransform = transform;
            _rigidbody.velocity = _currentVelocity * selfTransform.forward;
            _rigidbody.angularVelocity = _currentAngularVelocity * selfTransform.up;
        }
    }
}