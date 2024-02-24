using UnityEngine;

namespace PixelPirate.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        
        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantne = Instantiate(_prefab, _target.position, Quaternion.identity);
            var scale = _target.lossyScale;
            instantne.transform.localScale = scale;
            instantne.SetActive(true);
        }
    }
}