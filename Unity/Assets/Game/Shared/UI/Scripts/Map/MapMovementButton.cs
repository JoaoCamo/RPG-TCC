using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Game.Shared.Animation.UI;

namespace Game.Map
{
    public class MapMovementButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private ButtonSpriteAnimation buttonAnimation;

        public void Initialize(UnityAction onClick)
        {
            button.onClick.AddListener(onClick);
        }

        public void ToggleButton(bool mode)
        {
            button.interactable = mode;
            buttonAnimation.ToggleButton(mode);
        }
    }
}