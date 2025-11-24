using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public class DialogButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI textMesh;

        public void Initialize(UnityAction onClick, string text)
        {
            button.onClick.AddListener(onClick);
            textMesh.text = text;
        }
    }
}