using PixelPirateCodes.Components;
using UnityEngine;

namespace PixelPirateCodes.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private int _damage;
        [SerializeField] protected LayerMask _groundLayer;

        [Header("Checkers")]
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;
        
        protected Rigidbody2D _rigidbody;
        protected Vector2 _direaction;
        protected Animator _animator;
        protected bool _isGrounded = true;
        private bool _isJumping;
        
        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        
        public void SetDirection(Vector2 direction)
        {
            _direaction = direction;
        }

        protected virtual void Update()
        {
            _isGrounded = _groundCheck.IsTouchingLayer;
        }
        
        private void FixedUpdate() 
        {
            var xVelocity = _direaction.x * _speed;
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetBool(IsGroundKey, _isGrounded);
            _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);
            _animator.SetBool(IsRunning, _direaction.x != 0);
            
            UpdateSprinteDirection();
        }
        
        protected virtual float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direaction.y > 0;

            if (_isGrounded)
            {
                _isJumping = false;
            }
            
            if (isJumpPressing)
            {
                _isJumping = true;
                
                var isFalling = _rigidbody.velocity.y <= 0.001f;
                if (!isFalling) return yVelocity;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity): yVelocity;
            }
            else if (_rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {

            if (_isGrounded)
            {
                yVelocity += _jumpSpeed;
                _particles.Spawn("Jump");
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
        
        protected virtual void TakeDamage()
        {
            _isJumping = false;
            _animator.SetTrigger(Hit); 
            _direaction.y = 0f; 
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageVelocity);
        }
        
        protected virtual void Attack()
        {
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
    }
}