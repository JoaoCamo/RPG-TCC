using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Game.Shared.Animation.UI
{
    public class ButtonTextAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Color unselectedColor;
        [SerializeField] private Color selectedColor;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!button.interactable)
                return;

            textMesh.color = selectedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!button.interactable)
                return;

            textMesh.color = unselectedColor;
        }
    }
}