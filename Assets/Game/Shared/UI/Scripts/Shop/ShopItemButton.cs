using Game.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Shared.UI.Scripts.Shop
{
    public class ShopItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Button button;
        
        private readonly Color _selectedColor = new Color32(100, 100, 100, 255);
        private readonly Color _displayedItemColor = Color.yellow;
        private readonly Color _unselectedColor = Color.white;
        
        private ItemBase _itemBase;
        private bool _isItemDisplayed;
        
        public ItemBase ItemBase => _itemBase;
        public bool SetItemDisplayed { set => _isItemDisplayed = value; }
        
        public void Initialize(ItemBase itemBase, UnityAction onClick)
        {
            _itemBase = itemBase;
            textMesh.text = itemBase.ItemData.itemName;
            button.onClick.AddListener(onClick);
        }
        
        public void UpdateItemStatus(bool isDisplayed)
        {
            _isItemDisplayed = isDisplayed;
            textMesh.color = isDisplayed ? _displayedItemColor : _unselectedColor;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            textMesh.color = _selectedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            textMesh.color = _isItemDisplayed ? _displayedItemColor : _unselectedColor;
        }
    }
}