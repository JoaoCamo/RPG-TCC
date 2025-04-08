using Game.Character.Enum;
using UnityEngine;

namespace Game.Character.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private string _name;
        private EnemySize _size;
        private EnemyHealth _health;
        private CharacterEquipment _equipment;
        private CharacterInventory _inventory;
        private CharacterStats _stats;

        public string Name { get { return _name; } set { _name = value; } }
        public EnemySize Size { get { return _size; } set { _size = value; } }
        public EnemyHealth Health => _health;
        public CharacterEquipment Equipment => _equipment;
        public CharacterInventory Inventory => _inventory;
        public CharacterStats Stats => _stats;

        public void LoadEnemy(string name, EnemySize size)
        {
            _name = name;
            _size = size;
            _health = new EnemyHealth();
            _equipment = new CharacterEquipment();
            _inventory = new CharacterInventory();
            _stats = new CharacterStats();
        }
    }
}