using PixelPirateCodes.Components.Audio;
using PixelPirateCodes.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelPirateCodes.UI.Widgets
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _audioClip;

        private AudioSource _source;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_source == null)
                _source = AudioUtils.FindSfxSource();

            _source.PlayOneShot(_audioClip);
        }
    }
}