﻿using UnityEngine;
using Alexandria.ItemAPI;

namespace Items.Keywords
{
    class QuickKey : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Quick Keyword";

            string resourceName = "Items/Keywords/quick_key.png";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<QuickKey>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "Monogun Keyword";
            string longDesc = "Gives The Monogun 20% more reload speed.";

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ck");

            item.CanBeDropped = false;
            item.CanBeSold = false;
            item.quality = ItemQuality.EXCLUDED;
            item.sprite.IsPerpendicular = true;


        }
        
        public override void Pickup(PlayerController player)
        {

            base.Pickup(player);

        }
        public override DebrisObject Drop(PlayerController player)
        {
            DebrisObject debrisObject = base.Drop(player);
            return debrisObject;
        }
    }
}
