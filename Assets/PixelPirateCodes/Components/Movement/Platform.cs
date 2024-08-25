using UnityEngine;

namespace PixelPirateCodes.Components.Movement
{
    public class Platform : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.isTrigger) return;
            
            other.gameObject.transform.SetParent(gameObject.transform);
        }
        void OnTriggerExit2D(Collider2D other)
        {
            if(other.isTrigger) return;
            
            other.gameObject.transform.SetParent(null);
        }
    }
}

