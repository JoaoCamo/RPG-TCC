using UnityEngine;
using Game.Item;
using Game.Character.Enum;

namespace Game.Character.Data
{
    [CreateAssetMenu(menuName = "Character/Class Base Info")]
    public class CharacterClassInfo : ScriptableObject
    {
        public string className;
        public string classDescription;
        public ClassType type;
        public int hitDice;
        public ItemBase[] startingItems;
    }
}