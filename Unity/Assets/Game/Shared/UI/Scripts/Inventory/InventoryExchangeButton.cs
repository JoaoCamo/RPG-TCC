using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Game.UI
{
    public class InventoryExchangeButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Button button;

        public void Initialize(string name, UnityAction onClick)
        {
            textMesh.text = name;
            button.onClick.AddListener(onClick);
        }
    }
}