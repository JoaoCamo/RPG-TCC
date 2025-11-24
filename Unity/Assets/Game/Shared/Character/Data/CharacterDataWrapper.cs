namespace Assets.Game.Shared.Character.Data
{
    [System.Serializable]
    public struct CharacterDataWrapper
    {
        public string character;

        public CharacterDataWrapper(string character)
        {
            this.character = character;
        }
    }
}