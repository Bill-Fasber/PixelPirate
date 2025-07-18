using PixelPirateCodes.Components.Health;
using PixelPirateCodes.Utils.Disposables;
using UnityEngine;

namespace PixelPirateCodes.UI.Widgets
{
    public class LifeBarWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _lifeBar;
        [SerializeField] private HealthComponent _hp;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private int _maxHp;
<<<<<<< HEAD
=======
        
>>>>>>> c14b2051b1fccf81e70de703d6ce980bd4297d09
        private void Start()
        {
            if (_hp == null)
                _hp = GetComponentInParent<HealthComponent>();

            _maxHp = _hp.Health;

            _trash.Retain(_hp._onDie.Subscribe(OnDie));
<<<<<<< HEAD
            _trash.Retain(_hp._onChange.Subscribe(OnSpChanged));
        }

        private void OnDie()
        { 
            Destroy(gameObject);
        }

        private void OnSpChanged(int hp)
=======
            _trash.Retain(_hp._onChange.Subscribe(OnHpChanged));
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnHpChanged(int hp)
>>>>>>> c14b2051b1fccf81e70de703d6ce980bd4297d09
        {
            var progress = (float) hp / _maxHp;
            _lifeBar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}