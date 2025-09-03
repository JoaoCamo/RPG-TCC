using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace Game.Shared.Animation.UI
{
    public class ButtonTextAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Color unselectedColor;
        [SerializeField] private Color selectedColor;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            textMesh.color = selectedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            textMesh.color = unselectedColor;
        }
    }
}