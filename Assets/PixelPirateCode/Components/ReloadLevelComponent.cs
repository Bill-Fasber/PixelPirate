using PixelPirateCode.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelPirateCode.Components 
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
