using PixelPirateCodes.Model.Data;
using PixelPirateCodes.Model.Definitions;
using PixelPirateCodes.Utils;
using UnityEngine;

namespace PixelPirateCodes.Components.Colliectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetInterface<ICanAddInventory>();
            hero?.AddInInventory(_id, _count);
        }
    }
}