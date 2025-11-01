using System.Collections;
using Game.Shared.Main_Controllers;
using Game.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shared.UI.Scripts.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private ShopController shopController;
        [SerializeField] private ShopBuyUI shopBuyUI;
        [SerializeField] private ShopSellUI shopSellUI;
        [SerializeField] private ShopToggleButton toggleShopButton;
        [SerializeField] private TextMeshProUGUI toggleShopTextMesh;
        [SerializeField] private Button changeModeButton;
        [SerializeField] private TextMeshProUGUI changeModeTextMesh;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private bool _isInBuy = true;
        private bool _isOpen = false;

        private void Awake()
        {
            toggleShopButton.Initialize(ToggleUI);
            changeModeButton.onClick.AddListener(ChangeMode);
        }

        private void ToggleUI()
        {
            changeModeTextMesh.text = _isInBuy ? "Sell items" : "Buy items";
            
            _isOpen = !_isOpen;
            
            canvasGroup.alpha = _isOpen ? 1 : 0;
            canvasGroup.interactable = _isOpen;
            canvasGroup.blocksRaycasts = _isOpen;
            
            inventoryUI.ToggleInventoryButton(!_isOpen);
            StartCoroutine(CheckItemReload());
        }

        private void ToggleInnerUI()
        {
            GetToggleButtonText();
            
            if (_isOpen)
            {
                shopBuyUI.ToggleUI(_isInBuy, shopController.ShopItems.ToArray(), shopController.BuyItem);
                shopSellUI.ToggleUI(!_isInBuy, shopController.SellItem);
            }
            else
            {
                shopBuyUI.ClearSelectedItem();
                shopSellUI.ClearSelectedItem();
            }
        }

        private void ChangeMode()
        {
            _isInBuy = !_isInBuy;
            changeModeTextMesh.text = _isInBuy ? "Sell items" : "Buy items";
            
            shopBuyUI.ToggleUI(_isInBuy, shopController.ShopItems.ToArray(), shopController.BuyItem);
            shopSellUI.ToggleUI(!_isInBuy, shopController.SellItem);
        }

        private IEnumerator CheckItemReload()
        {
            if (!shopController.ReloadItems)
            {
                ToggleInnerUI();
                yield break;
            }
            
            yield return StartCoroutine(shopController.GetItems());
            shopController.ReloadItems = false;
            ToggleInnerUI();
        }

        private void GetToggleButtonText()
        {
            toggleShopTextMesh.text = _isOpen ? "Dialog" : "Shop";
        }

        public void ToggleShopButton(bool mode)
        {
            toggleShopButton.ToggleButton(mode);
        }
    }
}