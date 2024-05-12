using StrexetGames.TankVsMonsters.Scripts.Actors.Animations;
using StrexetGames.TankVsMonsters.Scripts.Actors.Common;
using StrexetGames.TankVsMonsters.Scripts.Actors.Common.MoveBehaviour;
using StrexetGames.TankVsMonsters.Scripts.Actors.NPC.Common;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Input;
using UnityEngine;
using UsefulTools.Runtime.DataStructures;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.NPC.RobotFighter
{
    public class RobotFighterMove : MonoBehaviour
    {
        [SerializeField] private MonoBehaviourImplementation<IMoveBehaviour> _moveBehaviour;
        [SerializeField] private MovementData _movementData;

        [Space]
        [SerializeField] private IAiBrain _brain;
        [SerializeField] private RobotFighterAnimator _animator;

        private IInputService _inputService;

        private IAttackerInputService InputService => _inputService;

        private void Awake() => _inputService = ServiceLocator.Container.Single<IInputService>();

        private void Update()
        {
            var movementAxis = InputService.MovementAxis;

            UpdateAnimator(movementAxis);
            _moveBehaviour.Implementation.UpdateMoveInput(movementAxis);
        }

        private void FixedUpdate() => _moveBehaviour.Implementation.Move(_movementData);

        private void UpdateAnimator(Vector2 movementAxis)
        {
            if (movementAxis.sqrMagnitude > Constants.Epsilon)
            {
                _animator.Move(_movementData.ForwardSpeed);
            }
            else
            {
                _animator.Move(0);
            }
        }
    }
}