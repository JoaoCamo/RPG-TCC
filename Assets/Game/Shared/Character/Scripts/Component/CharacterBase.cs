using Game.Character.Enum;
using Game.Character.Player;

namespace Game.Character
{
    public class CharacterBase
    {
        protected string _name;
        protected CharacterType _characterType;
        protected CharacterExperience _experience;
        protected CharacterEquipment _equipment;
        protected CharacterInventory _inventory;
        protected CharacterStats _stats;

        public string Name => _name;
        public CharacterType CharacterType => _characterType;
        public CharacterExperience Experience => _experience;
        public CharacterEquipment Equipment => _equipment;
        public CharacterInventory Inventory => _inventory;
        public CharacterStats Stats => _stats;

        public virtual void LoadCharacter(string name, CharacterType type, int level = 1)
        {
            _name = name;
            _characterType = type;
            _experience = new CharacterExperience(level);
            _equipment = new CharacterEquipment();
            _inventory = new CharacterInventory(this);
            _stats = new CharacterStats();
        }
    }
}