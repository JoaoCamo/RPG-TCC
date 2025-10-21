using Game.Static.Enum;

namespace Game.UI.Data
{
    [System.Serializable]
    public struct DialogOptionData
    {
        public string text;
        public GameState game_state;

        public DialogOptionData(string text, GameState gameState)
        {
            this.text = text;
            game_state = gameState;
        }
    }
}