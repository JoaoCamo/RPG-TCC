using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game.Shared.Animation.UI
{
    public class ButtonSpriteAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Image spriteRenderer;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            spriteRenderer.sprite = sprites[1];
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }
}