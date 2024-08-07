using System.Collections;
using PixelPirateCodes;
using PixelPirateCodes.Components;
using PixelPirateCodes.Creatures;
using UnityEngine;

namespace Assets.PixelPirateCodes.Creatures
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck _vision;
        [SerializeField] private ColliderCheck _canAttack;

        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _missCooldown = 1f;

        private IEnumerator _current;
        private GameObject _target;
        private bool _isDead;

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");
        private Patrol _patrol;

        public MobAI(ColliderCheck canAttack, ColliderCheck vision)
        {
            _canAttack = canAttack;
            _vision = vision;
        }

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if(_isDead) return;
            
            _target = go;
            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);
            
            StartState(GoToHero());
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget(); 
                }
                
                yield return null;
            }
            
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missCooldown);
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }
            
            StartState(GoToHero() );
        }

        private void SetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            _creature.SetDirection(direction.normalized);
        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);

            if (_current != null)
            {
                StopCoroutine(_current);   
            }

            _current = coroutine;
            StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDeadKey,true);
            
            if (_current != null)
                StopCoroutine(_current);
        }
    }
}