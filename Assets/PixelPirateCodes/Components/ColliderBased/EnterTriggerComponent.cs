using UnityEngine;

namespace PixelPirateCodes.Components.ColliderBased
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private EnterEvent _action;

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(string.IsNullOrEmpty(_tag) || other.gameObject.CompareTag(_tag)) 
            {
                _action?.Invoke(other.gameObject);
            }
        }
    }
}