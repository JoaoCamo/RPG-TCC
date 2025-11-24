using UnityEngine;
using DG.Tweening;

namespace Game.Static
{
    public static class StaticFunctions
    {
        private const Ease CanvasFadeEase = Ease.Linear;
        private const float CanvasFadeDelay = 0.5f;

        private static readonly int[] XpDrops = new int[] { 10, 200, 450, 700, 1100, 1800, 2300, 2900, 3900, 5000, 5900, 7200, 8400, 10000, 11500, 13000, 15000, 18000, 20000, 22000, 25000, 33000, 41000, 50000, 62000, 75000, 90000, 105000, 120000, 135000, 155000 };


        public static void ChangeCurrentUI(CanvasGroup newCanvas)
        {
            if(StaticVariables.CurrentCanvasGroup == newCanvas) 
                return;

            if(StaticVariables.CurrentCanvasGroup)
            {
                StaticVariables.CurrentCanvasGroup.interactable = false;
                StaticVariables.CurrentCanvasGroup.blocksRaycasts = false;
                StaticVariables.CurrentCanvasGroup.DOFade(0, CanvasFadeDelay).SetEase(CanvasFadeEase);

                newCanvas.interactable = true;
                newCanvas.blocksRaycasts = true;
                newCanvas.DOFade(1, CanvasFadeDelay).SetEase(CanvasFadeEase);
            }

            StaticVariables.CurrentCanvasGroup = newCanvas;
        }

        public static int GetXpValue(int challengeRating)
        {
            return XpDrops[challengeRating];
        }
    }
}