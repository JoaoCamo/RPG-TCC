using Game.Static;
using UnityEngine;

namespace Game.UI.Interface
{
    public interface IChangeUI
    {
        public void ChangeCurrentUI(CanvasGroup newCanvasToShow)
        {
            if(StaticVariables.currentCanvasGroup == newCanvasToShow) return;

            if(StaticVariables.currentCanvasGroup != null)
            {
                StaticVariables.currentCanvasGroup.interactable = false;
                StaticVariables.currentCanvasGroup.blocksRaycasts = false;
                StaticVariables.currentCanvasGroup.alpha = 0;
            }

            newCanvasToShow.interactable = true;
            newCanvasToShow.blocksRaycasts = true;
            newCanvasToShow.alpha = 1;

            StaticVariables.currentCanvasGroup = newCanvasToShow;
        }
    }
}