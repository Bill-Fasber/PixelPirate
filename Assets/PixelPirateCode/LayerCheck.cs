 using UnityEngine;

 namespace PixelPirateCode
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _grountLayer;
        private Collider2D _collider;

        public bool IsTouchingLayer;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            IsTouchingLayer = _collider.IsTouchingLayers(_grountLayer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsTouchingLayer = _collider.IsTouchingLayers(_grountLayer);
        }
    }
}