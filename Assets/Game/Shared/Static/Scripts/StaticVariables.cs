using UnityEngine;
using Game.UI.Data;
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
        public static CampaignStartInfo CampaignStartInfo;

        public static string CharacterHistory = string.Empty;
        public static string HistoryContext = string.Empty;
    }
}