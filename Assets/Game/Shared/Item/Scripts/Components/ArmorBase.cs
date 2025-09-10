using Game.Item.Data;
using Game.Static;

namespace Game.Item
{
    public class ArmorBase : ItemBase
    {
        private ArmorData _armorData;

        public ArmorData ArmorData => _armorData;

        public override void SetInfoItem(ArmorData armorData)
        {
            _armorData = armorData;
        }

        public override void UseItem()
        {
            StaticVariables.PlayerController.Equipment.EquipItem(this);
        }
    }
}