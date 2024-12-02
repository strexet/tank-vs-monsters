using UnityEngine;
using UsefulTools.Runtime.DataStructures.InterfaceImplementations;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.Common.MoveBehaviour
{
	public class BodyMoveBehaviour : MonoBehaviourImplementation<IMoveBehaviour>, IMoveBehaviour
	{
		[SerializeField] private Rigidbody _rigidbody;

		private Vector2 _movementAxis;

		public Vector3 CurrentPosition {
			get => _rigidbody.position;
			set => _rigidbody.MovePosition(value);
		}

		public void UpdateMoveInput(Vector2 movementAxis) => _movementAxis = movementAxis;

		public void Move(MovementData movementData)
		{
			var currentVelocity = movementData.ForwardSpeed * _movementAxis.y;
			var currentAngularVelocity = movementData.RotationSpeed * _movementAxis.x;

			var selfTransform = transform;

			_rigidbody.linearVelocity = currentVelocity * selfTransform.forward;
			_rigidbody.angularVelocity = currentAngularVelocity * selfTransform.up;
		}
	}
}