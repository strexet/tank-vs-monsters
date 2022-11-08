using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.Animators
{
    public class RobotFighterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Space]
        [SerializeField] private int _attacksCount;
        [SerializeField] private float _maxMoveRootSpeed;
        [SerializeField] private float _switchToRunThreshold = 0.5f;
        [SerializeField] private float _moveSpeedThreshold = 0.01f;

        private const string AttackTrigger = "Attack";
        private const string AttackIndex = "AttackIndex";
        private const string SpecialAttackTrigger = "SpecialAttack";

        private const string IsMoving = "IsMoving";
        private const string MoveSpeed = "MoveSpeed";

        private const string PickUpWhileRunningTrigger = "PickUpWhileRunning";
        private const string JumpAndHitTrigger = "JumpAndHit";

        private int _attackTrigger;
        private int _attackIndex;
        private int _specialAttackTrigger;
        private int _isMoving;
        private int _moveSpeed;
        private int _pickUpWhileRunningTrigger;
        private int _jumpAndHitTrigger;

        public bool IsRunning { get; private set; }

        private void Awake()
        {
            _attackTrigger = Animator.StringToHash(AttackTrigger);
            _attackIndex = Animator.StringToHash(AttackIndex);
            _specialAttackTrigger = Animator.StringToHash(SpecialAttackTrigger);
            _isMoving = Animator.StringToHash(IsMoving);
            _moveSpeed = Animator.StringToHash(MoveSpeed);
            _pickUpWhileRunningTrigger = Animator.StringToHash(PickUpWhileRunningTrigger);
            _jumpAndHitTrigger = Animator.StringToHash(JumpAndHitTrigger);
        }

        public void Move(float speed)
        {
            if (speed < _moveSpeedThreshold)
            {
                _animator.SetBool(_isMoving, false);
                _animator.SetFloat(_moveSpeed, 0);
                _animator.speed = 1;
                IsRunning = false;
            }
            else
            {
                var moveSpeed = GetMoveSpeed(speed);
                IsRunning = moveSpeed >= _switchToRunThreshold;

                if (moveSpeed > 1)
                {
                    _animator.speed = moveSpeed;
                    moveSpeed = 1;
                }

                _animator.SetBool(_isMoving, true);
                _animator.SetFloat(_moveSpeed, moveSpeed);
            }
        }

        public void Attack()
        {
            var attackIndex = Random.Range(0, _attacksCount);
            _animator.SetInteger(_attackIndex, attackIndex);
            _animator.SetTrigger(_attackTrigger);
        }

        public void SpecialAttack() => _animator.SetTrigger(_specialAttackTrigger);

        public void PickUpWhileRunning() => _animator.SetTrigger(_pickUpWhileRunningTrigger);

        public void JumpAndHit() => _animator.SetTrigger(_jumpAndHitTrigger);

        private float GetMoveSpeed(float speed) => speed / _maxMoveRootSpeed;
    }
}