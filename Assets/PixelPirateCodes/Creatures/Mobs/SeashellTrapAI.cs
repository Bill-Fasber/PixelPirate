using PixelPirateCodes.Components.Audio;
using PixelPirateCodes.Components.ColliderBased;
using PixelPirateCodes.Components.GoBased;
using PixelPirateCodes.Utils;
using UnityEngine;

namespace PixelPirateCodes.Creatures.Mobs
{
    public class SeashellTrapAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck _vision;

        [Header("Melee")]
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;
        
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
                if (_meleeCanAttack.IsTouchingLayer)
                {
                    if (_meleeCooldown.IsReady)
                        MeleeAttack();
                    return;
                }

                if (_rangeCooldown.IsReady)
                    RangeAttack();
            }
        }

        private void RangeAttack()
        {
            Sounds.Play("shoot");
            _rangeCooldown.Reset();
            _animator.SetTrigger(Range);
        }

        private void MeleeAttack()
        {
            Sounds.Play("melee");
            _meleeCooldown.Reset();
            _animator.SetTrigger(Melee);
        }

        private void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }
        
        private void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}
