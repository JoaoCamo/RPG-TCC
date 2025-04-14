using Game.Static.Enum;
using UnityEngine;

namespace Game.Static
{
    public static class StaticFunctions
    {
        public static void ChangeCurrentUI(CanvasGroup newCanvas)
        {
            if(StaticVariables.CurrentCanvasGroup == newCanvas) return;

            if(StaticVariables.CurrentCanvasGroup != null)
            {
                StaticVariables.CurrentCanvasGroup.interactable = false;
                StaticVariables.CurrentCanvasGroup.blocksRaycasts = false;
                StaticVariables.CurrentCanvasGroup.alpha = 0;
            }

            newCanvas.interactable = true;
            newCanvas.blocksRaycasts = true;
            newCanvas.alpha = 1;

            StaticVariables.CurrentCanvasGroup = newCanvas;
        }

        public static void LoadGameState(GameState newGameState)
        {
            StaticVariables.CurrentGameState = newGameState;
        }
    }
}