using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.Animations
{
	public class RootMotionDelegator : MonoBehaviour
	{
		[SerializeField] private Transform _parentTransform;
		[SerializeField] private Rigidbody _parentBody;
		[SerializeField] private Animator _animator;

		[Space]
		[SerializeField] private bool _useBodyMove;
		[SerializeField] private bool _useTransformMove;

		private void Awake() => _parentBody.isKinematic = true;

		private void OnAnimatorMove()
		{
			var delta = _animator.deltaPosition;

			if (_useTransformMove)
			{
				UpdateTransformPosition(delta);
			}

			if (_useBodyMove)
			{
				UpdateBodyPosition(delta);
			}
		}

		private void UpdateTransformPosition(Vector3 delta) => _parentTransform.position += delta;

		private void UpdateBodyPosition(Vector3 delta)
		{
			var position = _parentBody.position;
			var result = position + delta;

			_parentBody.MovePosition(result);
		}
	}
}