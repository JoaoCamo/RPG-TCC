using UnityEngine;

namespace Game.Static
{
    public static class StaticFunctions
    {
        public static void ChangeCurrentUI(CanvasGroup newCanvas)
        {
            if(StaticVariables.currentCanvasGroup == newCanvas) return;

            if(StaticVariables.currentCanvasGroup != null)
            {
                StaticVariables.currentCanvasGroup.interactable = false;
                StaticVariables.currentCanvasGroup.blocksRaycasts = false;
                StaticVariables.currentCanvasGroup.alpha = 0;
            }

            newCanvas.interactable = true;
            newCanvas.blocksRaycasts = true;
            newCanvas.alpha = 1;

            StaticVariables.currentCanvasGroup = newCanvas;
        }
    }
}