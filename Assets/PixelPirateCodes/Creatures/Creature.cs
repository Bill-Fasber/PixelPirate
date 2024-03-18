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
        
        protected Rigidbody2D Rigidbody;
        protected Vector2 Direaction;
        protected Animator Animator;
        protected bool IsGrounded;
        private bool _isJumping;
        
        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }
        
        public void SetDirection(Vector2 direction)
        {
            Direaction = direction;
        }
        
        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }
        
        private void FixedUpdate() 
        {
            var xVelocity = Direaction.x * _speed;
            var yVelocity = CalculateYVelocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
            Animator.SetBool(IsRunning, Direaction.x != 0);
            
            UpdateSprinteDirection();
        }
        
        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rigidbody.velocity.y;
            var isJumpPressing = Direaction.y > 0;

            if (IsGrounded)
            {
                _isJumping = false;
            }
            
            if (isJumpPressing)
            {
                _isJumping = true;
                
                var isFalling = Rigidbody.velocity.y <= 0.001f;
                if (!isFalling) return yVelocity;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity): yVelocity;
            }
            else if (Rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {

            if (IsGrounded)
            {
                yVelocity += _jumpSpeed;
                _particles.Spawn("Jump");
            }

            return yVelocity;
        }
        
        private void UpdateSprinteDirection()
        {
            if (Direaction.x > 0)
            { 
                transform.localScale = Vector2.one;
            }
            else if (Direaction.x < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
        
        protected virtual void TakeDamage()
        {
            _isJumping = false;
            Animator.SetTrigger(Hit); 
            Direaction.y = 0f; 
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }
        
        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
        }
        
        public virtual void OnDoAttack()
        {
            _attackRange.Check();
        }
    }
}