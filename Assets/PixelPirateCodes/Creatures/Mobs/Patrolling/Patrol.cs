using System.Collections;
using UnityEngine;

namespace PixelPirateCodes.Creatures
{
    public abstract class Patrol : MonoBehaviour

    {
        public abstract IEnumerator DoPatrol();
    }
}