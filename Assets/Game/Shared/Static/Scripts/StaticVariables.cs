using UnityEngine;
using Game.Static.Enum;
using Game.Character.Player;

namespace Game.Static
{
    public static class StaticVariables
    {
        public static GameState CurrentGameState;
        public static GameDifficulty GameDifficulty;
        public static PlayerController PlayerController;
        public static CanvasGroup CurrentCanvasGroup;
    }
}