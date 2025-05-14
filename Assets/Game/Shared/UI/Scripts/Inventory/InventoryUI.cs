using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private SelfInventoryUI selfInventoryUI;
        [SerializeField] private InventoryExchangeUI inventoryExchangeUI;
        [SerializeField] private Button changeModeButton;
        [SerializeField] private Button toggleInventoryButton;
        [SerializeField] private CanvasGroup canvasGroup;

        private bool _isInExchange = false;
        private bool _isOpen = false;

        private void Awake()
        {
            changeModeButton.onClick.AddListener(ToggleMode);
            toggleInventoryButton.onClick.AddListener(ToggleUI);
        }

        private void ToggleUI()
        {
            _isOpen = !_isOpen;

            canvasGroup.alpha = _isOpen ? 1 : 0;
            canvasGroup.interactable = _isOpen;
            canvasGroup.blocksRaycasts = _isOpen;

            selfInventoryUI.ToggleUI(!_isInExchange);
            inventoryExchangeUI.ToggleUI(_isInExchange);
        }

        private void ToggleMode()
        {
            _isInExchange = !_isInExchange;
            selfInventoryUI.ToggleUI(!_isInExchange);
            inventoryExchangeUI.ToggleUI(_isInExchange);
        }
    }  
}