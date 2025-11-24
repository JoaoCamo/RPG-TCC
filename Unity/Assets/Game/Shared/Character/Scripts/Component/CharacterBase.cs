using Game.Character.Enum;
using Game.Character;

namespace Game.Character
{
    public class CharacterBase
    {
        private string _name;
        private CharacterType _characterType;
        private CharacterEquipment _equipment;
        private CharacterInventory _inventory;
        private CharacterStats _stats;

        public string Name => _name;
        public CharacterType CharacterType => _characterType;
        public CharacterEquipment Equipment => _equipment;
        public CharacterInventory Inventory => _inventory;
        public CharacterStats Stats => _stats;

        protected void LoadCharacter(string name, CharacterType type)
        {
            _name = name;
            _characterType = type;
            _equipment = new CharacterEquipment();
            _inventory = new CharacterInventory(this);
            _stats = new CharacterStats();
        }
    }
}