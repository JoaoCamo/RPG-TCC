using Game.Static;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private SelfInventoryUI selfInventoryUI;
        [SerializeField] private InventoryExchangeUI inventoryExchangeUI;
        [SerializeField] private Button toggleInventoryButton;
        [SerializeField] private Button changeModeButton;
        [SerializeField] private TextMeshProUGUI inventoryButtonTextMesh;
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
            changeModeButton.gameObject.SetActive(StaticVariables.CurrentGameState == Static.Enum.GameState.Dungeon);

            _isOpen = !_isOpen;

            canvasGroup.alpha = _isOpen ? 1 : 0;
            canvasGroup.interactable = _isOpen;
            canvasGroup.blocksRaycasts = _isOpen;

            selfInventoryUI.ToggleUI(!_isInExchange);
            inventoryExchangeUI.ToggleUI(_isInExchange);
            GetButtonText();
        }

        private void ToggleMode()
        {
            _isInExchange = !_isInExchange;
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
    }  
}