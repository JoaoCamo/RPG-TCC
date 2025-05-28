using UnityEngine;
using DG.Tweening;

namespace Game.Static
{
    public static class StaticFunctions
    {
        private const Ease CANVAS_FADE_EASE = Ease.Linear;
        private const float CANVAS_FADE_DELAY = 0.5f;

        public static void ChangeCurrentUI(CanvasGroup newCanvas)
        {
            if(StaticVariables.CurrentCanvasGroup == newCanvas) return;

            if(StaticVariables.CurrentCanvasGroup != null)
            {
                StaticVariables.CurrentCanvasGroup.interactable = false;
                StaticVariables.CurrentCanvasGroup.blocksRaycasts = false;
                StaticVariables.CurrentCanvasGroup.DOFade(0, CANVAS_FADE_DELAY).SetEase(CANVAS_FADE_EASE);
            }

            newCanvas.interactable = true;
            newCanvas.blocksRaycasts = true;
            newCanvas.DOFade(1, CANVAS_FADE_DELAY).SetEase(CANVAS_FADE_EASE);

            StaticVariables.CurrentCanvasGroup = newCanvas;
        }
    }
}