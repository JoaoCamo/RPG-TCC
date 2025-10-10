namespace Game.Backend.Data
{
    [System.Serializable]
    public struct DialogSendData
    {
        public string choice;

        public DialogSendData(string choice)
        {
            this.choice = choice;
        }
    }
}