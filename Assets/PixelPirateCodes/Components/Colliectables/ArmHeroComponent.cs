using PixelPirateCodes.Creatures.Hero;
using PixelPirateCodes.Model;
using UnityEngine;

namespace PixelPirateCodes.Components.Colliectables
{
    public class ArmHeroComponent : MonoBehaviour
    {
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }

        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                hero.ArmHero();
                _session.Data.Sword += 1;
            }
        }
    }
}