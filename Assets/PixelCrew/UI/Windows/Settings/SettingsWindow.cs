using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
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

        private float _defaultMusic;
        private float _defaultSfx;
        
        private bool _flag;

        protected override void Awake()
        {
            base.Awake();
            OnSave(GameSettings.I.Music, GameSettings.I.Sfx);
        }

        protected override void Start()
        {
            base.Start();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }

        private void OnSave(FloatPersistentProperty modelMusic, FloatPersistentProperty modelSfx)
        {
            _defaultMusic = modelMusic.Value;
            _defaultSfx = modelSfx.Value;
        }
        
        public void OnMute()
        {
            switch (_flag)
            {
                case false:
                    _image.sprite = _offMute;
                    GameSettings.I.Music.Value = _defaultMusic;
                    GameSettings.I.Sfx.Value = _defaultSfx;
                    _flag = true;
                    break;
                
                case true:
                    _image.sprite = _onMute;
                    GameSettings.I.Music.Value = 0;
                    GameSettings.I.Sfx.Value = 0;
                    _flag = false;
                    break;
            }
        }
    }
}