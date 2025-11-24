namespace Game.Backend.Data
{
    public struct ItemRequestData
    {
        public string selectedModel;
        public string character;
        public string itemType;

        public ItemRequestData(string character, string itemType)
        {
            selectedModel = UnityEngine.PlayerPrefs.GetString("SELECTED_MODEL", "gpt-4.1");
            this.character = character;
            this.itemType = itemType;
        }
    }
}