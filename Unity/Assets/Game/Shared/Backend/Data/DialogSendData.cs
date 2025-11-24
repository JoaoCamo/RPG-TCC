namespace Game.Backend.Data
{
    [System.Serializable]
    public struct DialogSendData
    {
        public string selectedModel;
        public string choice;

        public DialogSendData(string choice)
        {
            selectedModel = UnityEngine.PlayerPrefs.GetString("SELECTED_MODEL", "gpt-4.1");
            this.choice = choice;
        }
    }
}