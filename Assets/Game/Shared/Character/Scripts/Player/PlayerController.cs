using Game.Character.Enum;
using UnityEngine;

namespace Game.Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        private string _name;
        private ClassType _class;
        private PlayerHealth _health;
        private CharacterEquipment _equipment;
        private CharacterInventory _inventory;
        private CharacterStats _stats;

        public string Name { get { return _name; } set { _name = value; } }
        public ClassType Class { get { return _class; } set { _class = value; } }
        public PlayerHealth Health => _health;
        public CharacterEquipment Equipment => _equipment;
        public CharacterInventory Inventory => _inventory;
        public CharacterStats Stats => _stats;

        public void LoadPlayer(string name, ClassType classType)
        {
            _name = name;
            _class = classType;
            _health = new PlayerHealth();
            _equipment = new CharacterEquipment();
            _inventory = new CharacterInventory();
            _stats = new CharacterStats();
        }
    }
}