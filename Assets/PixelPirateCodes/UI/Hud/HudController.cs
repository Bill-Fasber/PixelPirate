using PixelPirateCodes.Model;
using PixelPirateCodes.Model.Definitions;
using PixelPirateCodes.UI.Widgets;
using PixelPirateCodes.Utils;
using PixelPirateCodes.Utils.Disposables;
using UnityEngine;

namespace PixelPirateCodes.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

        private GameSession _session;
        
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.Data.Hp.Subscribe(OnHealthChanged));
            
            OnHealthChanged(_session.Data.Hp.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        public void OnSetting()
        {
            WindowUtils.CreateWindow("UI/InGameMenuWindow");
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
