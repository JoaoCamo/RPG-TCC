using Game.Item.Data;

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
    }
}