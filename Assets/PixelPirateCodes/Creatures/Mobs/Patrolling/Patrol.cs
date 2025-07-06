using System.Collections;
using UnityEngine;

namespace PixelPirateCodes.Creatures.Mobs.Patrolling
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}