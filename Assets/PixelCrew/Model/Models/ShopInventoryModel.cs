using System;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repositories.Items;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.Model.Models
{
    public class ShopInventoryModel : IDisposable
    {
        private readonly PlayerData _data;
        public readonly StringProperty InterfaceSelection = new StringProperty();

        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntProperty SelectedIndex = new IntProperty();

        public event Action OnChanged;

        public ShopInventoryModel(PlayerData data)
        {
            _data = data;

            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            _data.Inventory.OnChanged += OnChangedInventory;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {
            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            OnChanged?.Invoke();
        }
        
        public void Unlock(string id)
        {
            var def = DefsFacade.I.Items.Get(id);
            var isEnoughResources = _data.Inventory.IsEnough(def.Price);

            if (isEnoughResources)
            {
                _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
                _data.Perks.AddPerk(id);

                OnChanged?.Invoke();
            }
        }

        public bool CanBuy(string itemId)
        {
            var def = DefsFacade.I.Items.Get(itemId);
            return _data.Inventory.IsEnough(def.Price);
        }

        public void Dispose()
        {
            _data.Inventory.OnChanged -= OnChangedInventory;
        }

    }
}