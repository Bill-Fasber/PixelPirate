using System.Collections.Generic;
using PixelPirateCodes.Model;
using PixelPirateCodes.Model.Data;
using UnityEngine;

namespace PixelPirateCodes.Components.Collectables
{
    public class CollectorComponent : MonoBehaviour, ICanAddInInventory
    {
        [SerializeField] private List<InventoryItemData> _items = new List<InventoryItemData>();

        public void AddInInventory(string id, int value)
        {
            _items.Add(new InventoryItemData(id) {Value = value});
        }

        public void DropInInventory()
        {
            var session = FindObjectOfType<GameSession>();
            foreach (var inventoryItemData in _items)
            {
                session.Data.Inventory.Add(inventoryItemData.Id, inventoryItemData.Value);
            }

            _items.Clear();
        }
    }
}