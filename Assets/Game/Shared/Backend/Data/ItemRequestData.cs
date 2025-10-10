namespace Game.Backend.Data
{
    public struct ItemRequestData
    {
        public string character;
        public string itemType;

        public ItemRequestData(string character, string itemType)
        {
            this.character = character;
            this.itemType = itemType;
        }
    }
}