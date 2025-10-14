using UnityEngine;
using TMPro;
using Game.Item.Data;

namespace Game.Shared.UI.Scripts.Inventory
{
    public class InventoryArmorInfoUI : MonoBehaviour
    {
        [SerializeField] private GameObject armorArea;
        [SerializeField] private TextMeshProUGUI armorTypeTextMesh;
        [SerializeField] private TextMeshProUGUI armorValueTextMesh;

        public void SetInfo(ArmorData armorData)
        {
            armorTypeTextMesh.text = armorData.type.ToString();
            armorValueTextMesh.text = armorData.armorValue.ToString();
            armorArea.SetActive(true);
        }

        public void DisableUI()
        {
            armorArea.SetActive(false);
        }
    }
}