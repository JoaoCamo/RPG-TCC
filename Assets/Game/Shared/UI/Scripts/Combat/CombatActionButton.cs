using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Game.Shared.Animation.UI;

namespace Game.Shared.UI.Scripts.Combat
{
    public class CombatActionButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private ButtonSpriteAnimation buttonAnimation;

        public void UpdateButtonAction(UnityAction onClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onClick);
        }

        public void ToggleButton(bool mode)
        {
            button.interactable = mode;
            buttonAnimation.ToggleButton(mode);
        }

        public void ToggleDisplay(bool mode)
        {
            gameObject.SetActive(mode);
        }
    }
}