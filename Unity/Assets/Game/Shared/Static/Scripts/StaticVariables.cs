using UnityEngine;
using Game.UI.Data;
using Game.Static.Enum;
using Game.Character;

namespace Game.Static
{
    public static class StaticVariables
    {
        public const string APIPath = "https://rpg-tcc-api.onrender.com";
        
        public static GameState CurrentGameState;
        public static PlayerController PlayerController;
        public static CanvasGroup CurrentCanvasGroup;
        public static CampaignStartInfo CampaignStartInfo;

        public static string CharacterHistory = string.Empty;
        public static string HistoryContext = string.Empty;
    }
}