using Game.Item;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Shared.UI.Scripts.Inventory
{
    public class SelfInventoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Button button;

        private ItemBase _itemBase;
        private bool _isItemEquipped;

        private readonly Color _selectedColor = new Color32(100, 100, 100, 255);
        private readonly Color _equippedItemColor = Color.yellow;
        private readonly Color _unselectedColor = Color.white;

        public ItemBase ItemBase => _itemBase;
        public bool SetItemEquipped { set => _isItemEquipped = value; }

        public void Initialize(ItemBase itemBase, UnityAction onClick)
        {
            _itemBase = itemBase;
            textMesh.text = itemBase.ItemData.itemName;
            button.onClick.AddListener(onClick);
        }

        public void UpdateItemStatus(bool isEquipped)
        {
            _isItemEquipped = isEquipped;
            textMesh.color = _isItemEquipped ? _equippedItemColor : _unselectedColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            textMesh.color = _selectedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            textMesh.color = _isItemEquipped ? _equippedItemColor : _unselectedColor;
        }
    }
}