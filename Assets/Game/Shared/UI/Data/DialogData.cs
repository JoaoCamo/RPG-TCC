namespace Game.UI.Data
{
    [System.Serializable]
    public struct DialogData
    {
        public string name;
        public string dialogue;
        public DialogOptionData[] options;
    }

    [System.Serializable]
    public struct DialogDataWrapper
    {
        public string story;
    }
}