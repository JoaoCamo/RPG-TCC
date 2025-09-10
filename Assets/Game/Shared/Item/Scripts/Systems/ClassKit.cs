using System;
using Game.Item;
using Game.Character.Enum;
using Game.Character.Player;

namespace Game.Shared.Item.Scripts.Systems
{
    public static class ClassKit
    {
        public static void GiveClassKit(PlayerController playerController)
        {
            foreach (ItemBase item in GetItems(playerController.Class))
                playerController.Inventory.AddItem(item);
        }

        private static ItemBase[] GetItems(ClassType  classType)
        {
            return classType switch
            {
                _ => Array.Empty<ItemBase>()
            };
        }
    }
}