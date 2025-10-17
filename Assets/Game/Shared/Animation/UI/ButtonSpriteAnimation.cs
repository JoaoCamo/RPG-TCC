using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game.Shared.Animation.UI
{
    public class ButtonSpriteAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button button;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Image spriteRenderer;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            spriteRenderer.sprite = button.interactable ? sprites[1] : sprites[2];
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            spriteRenderer.sprite = button.interactable ? sprites[0] : sprites[2];
        }

        public void ToggleButton(bool mode)
        {
            spriteRenderer.sprite = mode ? sprites[0] : sprites[2];
        }
    }
}