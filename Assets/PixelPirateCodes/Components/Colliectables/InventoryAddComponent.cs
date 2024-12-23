using PixelPirateCodes.Creatures.Hero;
using UnityEngine;

namespace PixelPirateCodes.Components.Colliectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if(hero != null)
                hero.AddInInventory(_id, _count);
        }
    }
}