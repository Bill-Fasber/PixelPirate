using PixelPirate.Components;
using PixelPirate.Utils;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace PixelPirate
{
    public class Hero : MonoBehaviour
    {
        private bool _isArmed;
        private bool _flag = false;
        private float _coins;
        private float _currentCoolDownTime;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private int _damage;
        [SerializeField] private LayerMask _grountLayer;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactionLayer;

        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private Vector3 _groundCheckPositionDelta;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [SerializeField] private CheckCircleOverlap _attackRange;

        [SerializeField] private SpawnComponent _footStepRunParticles;
        [SerializeField] private SpawnComponent _footStepJumpParticles;
        [SerializeField] private SpawnComponent _footStepFallParticles;

        private Collider2D[] _interactionResult = new Collider2D[1];
        private Rigidbody2D _rigidbody;
        private Vector2 _direaction;
        private Animator _animator;
        private bool _isGrounded;
        private bool _allowDoubleJump;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");



        private void Awake() 
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public bool IsFlag()
        {
            return _flag;
        }

        public void SetDirection(Vector2 direction)
        {
            _direaction = direction;
        }

        private void Start()
        {
            _currentCoolDownTime = _cooldownTime;
        }

        private void Update()
        {
            _isGrounded = IsGrounded();
            if (_flag == true)
            {
                _currentCoolDownTime -= Time.deltaTime;
                if (_currentCoolDownTime <= 0)
                {
                    _flag = false;
                    _currentCoolDownTime = _cooldownTime;
                }
            }
        }

        private void FixedUpdate() 
        {
            var xVelocity = _direaction.x * _speed;
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetBool(IsGroundKey, _isGrounded);
            _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);
            _animator.SetBool(IsRunning, _direaction.x != 0);

            if (_isGrounded == true && yVelocity < -15f)
            {
                SpawFootFallDust();
            }

            UpdateSprinteDirection();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direaction.y > 0;

            if (_isGrounded) _allowDoubleJump = true;
            
            if (isJumpPressing)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_rigidbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigidbody.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;

            if (_isGrounded)
            {
                yVelocity += _jumpSpeed;
                SpawFootJumpDust();
            }
            else if (_allowDoubleJump)
            {
                yVelocity = _jumpSpeed;
                SpawFootJumpDust();
                _allowDoubleJump = false;
            }

            return yVelocity;
        }

        private void UpdateSprinteDirection()
        {
            if (_direaction.x > 0)
            { 
                transform.localScale = Vector2.one;
            }
            else if (_direaction.x < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }

        private bool IsGrounded() 
        {
            var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _grountLayer);
            return hit.collider != null; 
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = IsGrounded() ? HandlesUtils.TransparentGreen : HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position + _groundCheckPositionDelta, Vector3.forward, _groundCheckRadius);
        }
#endif
        

        public void SaySomething()
        { 
            Debug.Log("Something!");
        }

        public void AddCoins(int coins)
        {
            _coins += coins;
            Debug.Log($"{coins} coins added. total coins: {_coins}");
        }

        public void TakeDamage()
        {
            if (_flag == false)
            {
                _animator.SetTrigger(Hit);
                _direaction.y = 0f;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed);
                _flag = true;
            }

        }

        internal void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position, 
                _interactionRadius, 
                _interactionResult, 
                _interactionLayer);

            for (int i = 0; i < size; i++)
            {
                var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
                if(interactable != null)
                {
                    interactable.Interact();
                }
            }
        }

        public void SpawFootRunDust()
        {
            _footStepRunParticles.Spawn();
            
        }
        public void SpawFootJumpDust()
        {
            _footStepJumpParticles.Spawn();

        }
        public void SpawFootFallDust()
        {
            _footStepFallParticles.Spawn();
        }

        public void Attack()
        {
            if (!_isArmed) return;
            
            _animator.SetTrigger(AttackKey);
        }
        public void OnAttackKey()
        {
            var gos = _attackRange.GetObjectsInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null && go.CompareTag("Enemy"))
                {
                    hp.ModifyHealth(-_damage);
                }
            }
        }
        
        public void ArmHero()
        {
            _isArmed = true;
            _animator.runtimeAnimatorController = _armed;
        }
    }   
}

