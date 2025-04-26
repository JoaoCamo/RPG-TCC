using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private SelfInventoryUI selfInventoryUI;
        [SerializeField] private InventoryExchangeUI inventoryExchangeUI;
        [SerializeField] private Button changeModeButton;

        private bool _isInExchange = false;

        private void Awake()
        {
            changeModeButton.onClick.AddListener(ToggleMode);
        }

        private void ToggleMode()
        {
            _isInExchange = !_isInExchange;
            selfInventoryUI.ToggleUI(!_isInExchange);
            inventoryExchangeUI.ToggleUI(_isInExchange);
        }
    }  
}