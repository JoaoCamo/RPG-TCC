using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Item;
using Game.Character;
using Game.Static;

namespace Game.UI
{
    public class SelfInventoryButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Image outline;
        [SerializeField] private Button button;

        private ItemBase _itemBase;

        public void Initialize(ItemBase itemBase)
        {
            _itemBase = itemBase;
            textMesh.text = itemBase.ItemData.itemName;
        }

        public void OnClick()
        {
            _itemBase.UseItem();
            StaticEvents.OnItemUse();
        }

        public void UpdateOutline(CharacterEquipment characterEquipment)
        {
            outline.color = characterEquipment.CheckForItem(_itemBase) ? Color.yellow : Color.white;
        }
    }
}