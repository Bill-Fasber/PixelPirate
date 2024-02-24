using PixelPirateC_.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelPirate.Components 
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            session.LoadLastSave();
            
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
