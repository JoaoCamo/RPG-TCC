using UnityEngine;

namespace Game.UI
{
    public class SelfInventoryUI : MonoBehaviour
    {
        [SerializeField] private SelfInventoryButton selfInventoryButtonPrefab;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private CanvasGroup canvasGroup;

        public void ToggleUI(bool mode)
        {
            canvasGroup.alpha = mode ? 1 : 0;
            canvasGroup.interactable = mode;
            canvasGroup.blocksRaycasts = mode;
        }
    }
}