using System;
using PixelPirateCode.Components;
using UnityEngine;

namespace PixelPirateCode.Creatures
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private int _damage; 
        [SerializeField] private LayerMask _grountLayer;
        
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] private SpawnListComponent _particles;
        
        protected Rigidbody2D _rigidbody;
        private Vector2 _direaction;
        private Animator _animator;
        private bool _isGrounded;
        
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

    }
}