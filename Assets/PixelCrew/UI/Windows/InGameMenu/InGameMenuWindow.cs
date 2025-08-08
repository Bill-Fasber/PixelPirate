using PixelCrew.Model;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.Windows.InGameMenu
{
    public class InGameMenuWindow : AnimatedWindow
    {
        private float _defaultTimeScale;

        protected override void Start()
        {
            base.Start();

            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void OnShowSettings()
        {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
<<<<<<< HEAD
        
        public void OnShowLocalization()
        {
            WindowUtils.CreateWindow("UI/LocalizationWindow");
        }
=======
>>>>>>> d84cd14e979ec79cfeb818536bdaa39b6c05abbc
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

        public void OnExit()
        {
            SceneManager.LoadScene("MainMenu");

            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }
    }
}