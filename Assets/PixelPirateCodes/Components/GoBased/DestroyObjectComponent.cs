using UnityEngine;

namespace PixelPirateCodes.Components.GoBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        public void DestroyObject()
        {
            Destroy(_objectToDestroy);
        }
    }
}


