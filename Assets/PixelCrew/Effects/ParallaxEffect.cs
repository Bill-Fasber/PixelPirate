using UnityEngine;

namespace PixelCrew.Effects
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float _effectValue;
        [SerializeField] private Transform _followTarget;

        private float _startX;

        private void Start()
        {
            _startX = transform.position.x;
        }

        private void LateUpdate()
        {
            var transform1 = transform;
            var currentPosition = transform1.position;
            var deltaX = _followTarget.position.x * _effectValue;
            transform1.position = new Vector3(_startX + deltaX, currentPosition.y, currentPosition.z);
        }
    }
}