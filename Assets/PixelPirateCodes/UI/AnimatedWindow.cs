using UnityEngine;

namespace PixelPirateCodes.UI
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            _animator.SetTrigger(Show);
        }

        public void Close()
        {
            _animator.SetTrigger(Hide);
        }

        public void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}