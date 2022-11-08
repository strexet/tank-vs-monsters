using Actors.Data;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;
using UsefulTools.Runtime.DataStructures;

namespace _Game_.Scripts.Actors.Player
{
    public class PlayerMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonoBehaviourImplementation<IMoveBehaviour> _moveBehaviour;
        [SerializeField] private MovementData _movementData;
        [SerializeField] private CharacterData _characterData;

        private IInputService _inputService;

        private void Awake() => _inputService = ServiceLocator.Container.Single<IInputService>();

        private void Update() => _moveBehaviour.Implementation.UpdateMoveInput(_inputService.MovementAxis);

        private void FixedUpdate() => _moveBehaviour.Implementation.Move(_movementData);

        public void LoadProgress(PlayerProgress progress)
        {
            var savedPositionOnLevel = progress.WorldData.PlayerPositionOnLevel;

            if (string.Equals(savedPositionOnLevel.LevelName, CurrentLevelName()) &&
                savedPositionOnLevel.Position != null)
            {
                _moveBehaviour.Implementation.CurrentPosition = savedPositionOnLevel.Position;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            var currentLevelName = CurrentLevelName();
            var currentPosition = _moveBehaviour.Implementation.CurrentPosition;
            var positionOnLevel = new PositionOnLevel(currentLevelName, currentPosition.AddY(_characterData.Height));

            progress.WorldData.PlayerPositionOnLevel = positionOnLevel;
        }

        private static string CurrentLevelName() => SceneManager.GetActiveScene().name;
    }
}