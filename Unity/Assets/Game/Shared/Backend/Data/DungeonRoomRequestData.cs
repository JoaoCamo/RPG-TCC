namespace Game.Backend.Data
{
    [System.Serializable]
    public struct DungeonRoomRequestData
    {
        public string selectedModel;
        public string context;

        public DungeonRoomRequestData(string context)
        {
            selectedModel = UnityEngine.PlayerPrefs.GetString("SELECTED_MODEL", "gpt-4.1");
            this.context = context;
        }
    }
}