using System;
using Infrastructure;
using Infrastructure.Core;
using Services.Input;
using UnityEngine;

namespace Player
{
    public class PlayerCharacterMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private MovementData _movementData;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            Vector3 movementVector = Vector3.zero;

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
            movementVector += Physics.gravity;
            _characterController.Move(Time.fixedDeltaTime * _movementData.ForwardSpeed * movementVector);
        }
    }
}