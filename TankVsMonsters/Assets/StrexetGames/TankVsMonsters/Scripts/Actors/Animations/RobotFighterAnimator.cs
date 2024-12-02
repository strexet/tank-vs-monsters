using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.Animations
{
	public class RobotFighterAnimator : MonoBehaviour, IAnimationStateReader
	{
		private readonly static int IsMoving = Animator.StringToHash("IsMoving");
		private readonly static int MoveSpeed = Animator.StringToHash("MoveSpeed");

		private readonly static int CombatStatus = Animator.StringToHash("CombatStatus");
		private readonly static int InjuryStatus = Animator.StringToHash("InjuryStatus");

		private readonly static int AttackTrigger = Animator.StringToHash("Attack");
		private readonly static int AttackIndex = Animator.StringToHash("AttackIndex");
		private readonly static int SpecialAttackTrigger = Animator.StringToHash("SpecialAttack");

		private readonly static int PickUpWhileMovingTrigger = Animator.StringToHash("PickUpWhileMoving");
		private readonly static int JumpAndHitTrigger = Animator.StringToHash("JumpAndHit");
		private readonly static int SlideTrigger = Animator.StringToHash("Slide");

		private readonly static int KnockdownTrigger = Animator.StringToHash("Knockdown");
		private readonly static int GetUpTrigger = Animator.StringToHash("GetUp");

		private readonly static int HitTrigger = Animator.StringToHash("Hit");
		private readonly static int DieTrigger = Animator.StringToHash("Die");

		private readonly static int _idleStateHash = Animator.StringToHash("Idle Blend Tree");
		private readonly static int _attackStateHash = Animator.StringToHash("Attack StateMachine");
		private readonly static int _movingStateHash = Animator.StringToHash("Move Blend Tree");
		private readonly static int _deathStateHash = Animator.StringToHash("Walking To Death");

		[SerializeField] private Animator _animator;

		[Space]
		[SerializeField] private int _attacksCount;
		[SerializeField] private float _maxMoveRootSpeed;
		[SerializeField] private float _switchToRunThreshold = 0.5f;
		[SerializeField] private float _moveSpeedThreshold = 0.01f;

		public AnimatorState State { get; private set; }

		public event Action<AnimatorState> StateEntered;

		public event Action<AnimatorState> StateExited;

		public bool IsRunning { get; private set; }

		private void Start() => PlayAnimations(5.5f).Forget();

		private float _nextSwitchAnimationTime;

		private async UniTaskVoid PlayAnimations(float delay)
		{
			_nextSwitchAnimationTime = Time.time + delay;
			await WaitForAnimationFinish(delay);

			SetCombatStatus(0);
			StopMoving();
			await WaitForAnimationFinish(delay);

			Move(_moveSpeedThreshold * 2);
			await WaitForAnimationFinish(delay);


			Move(_maxMoveRootSpeed);
			await WaitForAnimationFinish(delay);

			SetCombatStatus(1);
			StopMoving();

			for (var i = 0; i < 10; i++)
			{
				Attack();
				await WaitForAnimationFinish(delay);
			}
		}

		private async UniTask WaitForAnimationFinish(float delay)
		{
			await UniTask.WaitWhile(() => Time.time < _nextSwitchAnimationTime);
			_nextSwitchAnimationTime = Time.time + delay;
			Rotate180();
		}

		private void Rotate180() => _animator.transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);

		public void Move(float speed)
		{
			if (speed < _moveSpeedThreshold)
			{
				StopMoving();
			}
			else
			{
				var moveSpeed = GetMoveSpeed(speed);
				IsRunning = moveSpeed >= _switchToRunThreshold;

				if (moveSpeed > 1)
				{
					// _animator.speed = moveSpeed;
					moveSpeed = 1;
				}

				_animator.SetBool(IsMoving, true);
				_animator.SetFloat(MoveSpeed, moveSpeed);
			}
		}

		public void StopMoving()
		{
			_animator.SetBool(IsMoving, false);
			// _animator.SetFloat(MoveSpeed, 0);
			// _animator.speed = 1;
			IsRunning = false;
		}

		public void Attack()
		{
			var attackIndex = Random.Range(0, _attacksCount);
			_animator.SetInteger(AttackIndex, attackIndex);
			_animator.SetTrigger(AttackTrigger);
		}

		public void SpecialAttack() => _animator.SetTrigger(SpecialAttackTrigger);

		public void PickUpWhileRunning() => _animator.SetTrigger(PickUpWhileMovingTrigger);

		public void JumpAndHit() => _animator.SetTrigger(JumpAndHitTrigger);

		public void Slide() => _animator.SetTrigger(SlideTrigger);

		public void SetInjuryStatus(float value) => _animator.SetFloat(InjuryStatus, value);

		public void SetCombatStatus(float value) => _animator.SetFloat(CombatStatus, value);

		public void Knockdown() => _animator.SetTrigger(KnockdownTrigger);

		public void GetUp() => _animator.SetTrigger(GetUpTrigger);

		public void Hit() => _animator.SetTrigger(HitTrigger);

		public void Die() => _animator.SetTrigger(DieTrigger);

		private float GetMoveSpeed(float speed) => speed / _maxMoveRootSpeed;

		public void EnteredState(int stateHash)
		{
			State = StateFor(stateHash);
			StateEntered?.Invoke(State);
		}

		public void ExitedState(int stateHash) => StateExited?.Invoke(StateFor(stateHash));

		private AnimatorState StateFor(int stateHash)
		{
			AnimatorState state;

			if (stateHash == _idleStateHash)
			{
				state = AnimatorState.Idle;
			}
			else if (stateHash == _attackStateHash)
			{
				state = AnimatorState.Attack;
			}
			else if (stateHash == _movingStateHash)
			{
				state = AnimatorState.Moving;
			}
			else if (stateHash == _deathStateHash)
			{
				state = AnimatorState.Died;
			}
			else
			{
				state = AnimatorState.Unknown;
			}

			return state;
		}
	}
}