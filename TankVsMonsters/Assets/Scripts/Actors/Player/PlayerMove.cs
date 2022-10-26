using Infrastructure.Services;
using Infrastructure.Services.Input;
using UnityEngine;
using UsefulTools.Runtime.DataStructures;

namespace Actors.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private MonoBehaviourImplementation<IMoveBehaviour> _moveBehaviour;
        [SerializeField] private MovementData _movementData;

        private IInputService _inputService;

        private void Awake() => _inputService = ServiceLocator.Container.Single<IInputService>();

        private void Update() => _moveBehaviour.Implementation.UpdateMoveInput(_inputService.MovementAxis);

        private void FixedUpdate() => _moveBehaviour.Implementation.Move(_movementData);
    }
}