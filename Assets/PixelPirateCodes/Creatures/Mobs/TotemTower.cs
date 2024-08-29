using System.Collections.Generic;
using System.Linq;
using PixelPirateCodes.Components.Health;
using PixelPirateCodes.Utils;
using UnityEngine;

namespace PixelPirateCodes.Creatures.Mobs
{
    public class TotemTower : MonoBehaviour
    {
        [SerializeField] private List<TotemTrapAI> _traps;
        [SerializeField] private Cooldown _cooldown;

        private int _currentTrap;

        private void Start()
        {
            foreach (var totemTrapAI in _traps)
            {
                totemTrapAI.enabled = false;
                var hp = totemTrapAI.GetComponent<HealthComponent>();
                hp._onDie.AddListener(() => OnTrapDie(totemTrapAI));
            }
        }

        private void OnTrapDie(TotemTrapAI totemTrapAI)
        {
            var index = _traps.IndexOf(totemTrapAI);
            _traps.Remove(totemTrapAI);
            if (index < _currentTrap)
            {
                _currentTrap--;
            }
        }

        private void Update()
        {
            if (_traps.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }
            
            var hasAnyTarget = _traps.Any(x => x._vision.IsTouchingLayer);
            if (hasAnyTarget)
            {
                if (_cooldown.IsReady)
                {
                    _traps[_currentTrap].RangeAttack();
                    _cooldown.Reset();
                    _currentTrap = (int) Mathf.Repeat(_currentTrap + 1, _traps.Count);
                }
            }
        }
    }
}