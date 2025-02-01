using UnityEngine;

namespace PixelPirateCodes.Components.Audio
{
    public class PlaySfxSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private AudioSource _source;

        public void Play()
        {
        }
    }
}