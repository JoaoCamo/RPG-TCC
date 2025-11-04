using Game.Shared.UI.Scripts.Inventory;
using Game.Shared.UI.Scripts.Shop;
using Game.Static;
using Game.Static.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private ShopUI shopUI;
        [SerializeField] private SelfInventoryUI selfInventoryUI;
        [SerializeField] private InventoryExchangeUI inventoryExchangeUI;
        [SerializeField] private InventoryToggleButton inventoryToggleButton;
        [SerializeField] private Button changeModeButton;
        [SerializeField] private TextMeshProUGUI inventoryButtonTextMesh;
        [SerializeField] private TextMeshProUGUI changeModeButtonTextMesh;
        [SerializeField] private CanvasGroup canvasGroup;

        private bool _isInExchange = false;
        private bool _isOpen = false;

        private void Awake()
        {
            changeModeButton.onClick.AddListener(ToggleMode);
            inventoryToggleButton.Initialize(ToggleUI);
        }

        private void ToggleUI()
        {
            ToggleChangeModeButton();

            _isOpen = !_isOpen;
            _isInExchange &= StaticVariables.CurrentGameState == GameState.Dungeon;

            canvasGroup.alpha = _isOpen ? 1 : 0;
            canvasGroup.interactable = _isOpen;
            canvasGroup.blocksRaycasts = _isOpen;
            
            shopUI.ToggleShopButton(!_isOpen && StaticVariables.CurrentGameState == GameState.Dialogue);
            GetButtonText();

            if (_isOpen)
            {
                selfInventoryUI.ToggleUI(!_isInExchange);
                inventoryExchangeUI.ToggleUI(_isInExchange);
            }
            else
                selfInventoryUI.ClearSelectedItem();
        }

        private void ToggleChangeModeButton()
        {
            bool dungeonInventory = StaticVariables.CurrentGameState == Static.Enum.GameState.Dungeon;

            changeModeButton.interactable = dungeonInventory;
            changeModeButtonTextMesh.text = !dungeonInventory ? "Inventory" : _isInExchange ? "Return to Inventory" : "Search floor for items";
        }

        private void ToggleMode()
        {
            _isInExchange = !_isInExchange;
            changeModeButtonTextMesh.text = _isInExchange ? "Return to Inventory" : "Search floor for items";
            selfInventoryUI.ToggleUI(!_isInExchange);
            inventoryExchangeUI.ToggleUI(_isInExchange);
        }

        private void GetButtonText()
        {
            if (_isOpen)
                inventoryButtonTextMesh.text = StaticVariables.CurrentGameState == Static.Enum.GameState.Dungeon ? "Dungeon" : "Dialog";
            else
                inventoryButtonTextMesh.text = "Inventory";
        }

        public void ToggleInventoryButton(bool mode)
        {
            inventoryToggleButton.ToggleButton(mode);
        }
    }  
}