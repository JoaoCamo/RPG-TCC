namespace Game.Shared.Backend.Data
{
    [System.Serializable]
    public struct HistoryArcRequestData
    {
        public string lastPlayerAction;
        public int currentArc;
        public bool isEnding;

        public HistoryArcRequestData(string lastPlayerAction, int currentArc, bool isEnding)
        {
            this.lastPlayerAction = lastPlayerAction;
            this.currentArc = currentArc;
            this.isEnding = isEnding;
        }
    }
}