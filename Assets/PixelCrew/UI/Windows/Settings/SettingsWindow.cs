using PixelCrew.Model.Data;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Settings
{
    public class SettingsWindow : AnimatedWindow
    {
        [Space] [Header("Sliders")]
        [SerializeField] private AudioSettingsSliderWidget _music;
        
        [SerializeField] private AudioSettingsSliderWidget _sfx;

        [Space] [Header("Mute")] 
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _onMute;
        [SerializeField] private Sprite _offMute;

        private float _lastMusicVolume = 0.5f;
        private float _lastSfxVolume = 0.5f;

        private bool _isMute;
        
        protected override void Start()
        {
            base.Start();
            if (_isMute == true) return;
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }

        public void OnMute()
        {
            switch (_isMute)
            {
                case true:
                    _image.sprite = _onMute;
                    _lastMusicVolume = GameSettings.I.Music.Value;
                    _lastSfxVolume = GameSettings.I.Sfx.Value;
                    GameSettings.I.Music.Value = 0;
                    GameSettings.I.Sfx.Value = 0;
                    _isMute = false;
                    break;
                
                case false:
                    _image.sprite = _offMute;
                    GameSettings.I.Music.Value = _lastMusicVolume;
                    GameSettings.I.Sfx.Value = _lastSfxVolume;
                    _lastMusicVolume = GameSettings.I.Music.Value;
                    _lastSfxVolume = GameSettings.I.Sfx.Value;
                    _isMute = true;
                    break;
            }
        }
    }
}