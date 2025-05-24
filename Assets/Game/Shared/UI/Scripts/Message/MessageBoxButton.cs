using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.UI.Data;

namespace Game.UI
{
    public class MessageBoxButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI textMesh;

        public void SetButton(MessageBoxButtonData data)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(data.onClick);
            textMesh.text = data.text;
        }
    }
}