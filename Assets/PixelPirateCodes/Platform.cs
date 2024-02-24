using UnityEngine;

namespace PixelPirateCodes
{
    public class Platform : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.transform.SetParent(gameObject.transform);
        }
        void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}

