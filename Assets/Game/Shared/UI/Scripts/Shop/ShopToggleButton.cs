using Game.Shared.Animation.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Shared.UI.Scripts.Shop
{
    public class ShopToggleButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private ButtonTextAnimation buttonAnimation;
        
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