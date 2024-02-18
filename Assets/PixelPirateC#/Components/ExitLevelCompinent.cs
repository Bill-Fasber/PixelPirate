using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelPirate.Components
{
    public class ExitLevelCompinent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        public void Exit()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}