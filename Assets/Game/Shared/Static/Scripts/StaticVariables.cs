using UnityEngine;
using Game.Static.Enum;
using Game.Map;

namespace Game.Static
{
    public static class StaticVariables
    {
        public static GameState CurrentGameState;
        public static GameDifficulty GameDifficulty;
        public static MapSection[,] CurrentMap;
        public static CanvasGroup currentCanvasGroup;
    }
}