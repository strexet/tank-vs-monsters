using Input.Generated;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Input
{
    public class GameInputService : IInputService, PlayerInputActions.IGameplayActions
    {
        private readonly PlayerInputActions _playerInputActions;
        
        public Vector2 MovementAxis { get; private set; }
        public bool IsAttackButtonPressed { get; private set; }

        public GameInputService()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Gameplay.SetCallbacks(this);
        }

        public void Enable() =>
            _playerInputActions.Enable();

        public void Disable() =>
            _playerInputActions.Disable();
        
        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementAxis = context.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsAttackButtonPressed = true;
            }
            else if (context.canceled)
            {
                IsAttackButtonPressed = false;
            }
        }
    }
}