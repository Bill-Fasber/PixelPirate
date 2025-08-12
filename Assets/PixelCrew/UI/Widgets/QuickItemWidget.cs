using PixelCrew.Model.Definitions;
using UnityEngine;
using UnityEngine.UI;
using ItemWithCount = PixelCrew.Model.Definitions.Repositories.ItemWithCount;

namespace PixelCrew.UI.Widgets
{
    public class QuickItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _value;

        public void SetData(ItemWithCount price)
        {
            var def = DefsFacade.I.Items.Get(price.ItemId);
            _icon.sprite = def.Icon;

            _value.text = price.Count.ToString();
        }
    }
}