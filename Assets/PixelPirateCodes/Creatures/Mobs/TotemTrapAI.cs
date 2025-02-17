﻿using PixelPirateCodes.Components.Audio;
using PixelPirateCodes.Components.ColliderBased;
using PixelPirateCodes.Components.GoBased;
using PixelPirateCodes.Utils;
using UnityEngine;

namespace PixelPirateCodes.Creatures.Mobs
{
    public class TotemTrapAI : MonoBehaviour
    {
        [SerializeField] public ColliderCheck _vision;

        [Header("Range")]
        [SerializeField] private Cooldown _rangeCooldown;
        [SerializeField] private SpawnComponent _rangeAttack;
        
        protected PlaySoundsComponent Sounds;
        
        private static readonly int Melee = Animator.StringToHash("melee");
        private static readonly int Range = Animator.StringToHash("range");

        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }

        private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                if (_rangeCooldown.IsReady)
                    RangeAttack();
            }
        }

        public void RangeAttack()
        {
            Sounds.Play("range");
            _rangeCooldown.Reset();
            _animator.SetTrigger(Range);
        }

        private void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}
