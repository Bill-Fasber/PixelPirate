using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.Model.Definitions.Repositories.Items;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.ShopWindow
{
    public class ManageShopWindow : AnimatedWindow
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Text _infoText;
        [SerializeField] private ShopItemWidget _price;
        
        private PredefinedDataGroup<ItemDef, ShopItemWidget> _dataGroup;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private GameSession _session;

        protected override void Start()
        {
            base.Start();
            
            _dataGroup = new PredefinedDataGroup<ItemDef, ShopItemWidget>(_container);
            _session = FindObjectOfType<GameSession>();

            _trash.Retain(_session.QuickInventory.Subscribe(OnShopChanged));

            _trash.Retain(_buyButton.onClick.Subscribe(OnBuy));

            OnShopChanged();
        }

        private void OnShopChanged()
        {
            _dataGroup.SetData(DefsFacade.I.Items.All);

            var selected = _session.PerksModel.InterfaceSelection.Value;

            _buyButton.gameObject.SetActive(!_session.PerksModel.IsUnlocked(selected));
            _buyButton.interactable = _session.PerksModel.CanBuy(selected);

            var def = DefsFacade.I.Items.Get(selected);
            _price.SetData(def.Price);

            _infoText.text = LocalizationManager.I.Localize(def.Info);
        }
        
        private void OnBuy()
        {
            throw new System.NotImplementedException();
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}