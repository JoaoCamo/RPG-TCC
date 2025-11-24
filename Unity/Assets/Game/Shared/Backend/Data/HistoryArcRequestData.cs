namespace Game.Shared.Backend.Data
{
    [System.Serializable]
    public struct HistoryArcRequestData
    {
        public string selectedModel;
        public string lastPlayerAction;
        public int currentArc;
        public bool isEnding;

        public HistoryArcRequestData(string lastPlayerAction, int currentArc, bool isEnding)
        {
            selectedModel = UnityEngine.PlayerPrefs.GetString("SELECTED_MODEL", "gpt-4.1");
            this.lastPlayerAction = lastPlayerAction;
            this.currentArc = currentArc;
            this.isEnding = isEnding;
        }
    }
}