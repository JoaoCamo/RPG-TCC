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
        [SerializeField] private Color disabledColor;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!button.interactable)
                return;

            textMesh.color = button.interactable ? selectedColor : disabledColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!button.interactable)
                return;

            textMesh.color = button.interactable ? unselectedColor : disabledColor;
        }
        
        public void ToggleButton(bool mode)
        {
            textMesh.color = mode ? unselectedColor : disabledColor;
        }
    }
}