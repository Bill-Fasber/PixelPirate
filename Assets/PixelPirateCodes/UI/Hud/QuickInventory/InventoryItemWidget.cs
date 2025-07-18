using PixelPirateCodes.Model;
using PixelPirateCodes.Model.Data;
using PixelPirateCodes.Model.Definitions;
using PixelPirateCodes.Model.Definitions.Repositories.Items;
using PixelPirateCodes.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelPirateCodes.UI.Hud.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selection;
        [SerializeField] private Text _value;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private int _index;

        private void Start()
        {
            var session = FindObjectOfType<GameSession>();
            session.QuickInventory.SelectedIndex.SubscribeAndInvoke(OnIndexChanged);
        }

        private void OnIndexChanged(int newValue, int _)
        {
            _selection.SetActive(_index == newValue);
        }

        public void SetData(InventoryItemData item, int index)
        {
            _index = index;
            var def = DefsFacade.I.Items.Get(item.Id);
            _icon.sprite = def.Icon;
            _value.text = def.HasTag(ItemTag.Stackable) ? item.Value.ToString() : string.Empty;
        }
    }
}