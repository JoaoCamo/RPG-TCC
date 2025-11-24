using Game.Item.Data;
using TMPro;
using UnityEngine;

namespace Game.Shared.UI.Scripts.Shop
{
    public class ShopArmorInfoUI : MonoBehaviour
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