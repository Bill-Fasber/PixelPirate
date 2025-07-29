using PixelPirateCodes.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelPirateCodes.Components.LevelManagement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            SceneManager.LoadScene(_sceneName);
        }
    }
}