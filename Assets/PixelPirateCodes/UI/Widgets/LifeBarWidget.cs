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
        private void Start()
        {
            if (_hp == null)
                _hp = GetComponentInParent<HealthComponent>();

            _maxHp = _hp.Health;

            _trash.Retain(_hp._onDie.Subscribe(OnDie));
            _trash.Retain(_hp._onChange.Subscribe(OnSpChanged));
        }

        private void OnDie()
        { 
            Destroy(gameObject);
        }

        private void OnSpChanged(int hp)
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