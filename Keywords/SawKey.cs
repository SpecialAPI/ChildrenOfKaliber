﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Gungeon;

namespace Items.Keywords
{
    class SawKey : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Sawblade Type";

            string resourceName = "Items/Keywords/sawblade_key.png";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<SawKey>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "Monogun Type";
            string longDesc = "Gives The Monogun Sawblade type.";

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "cel");

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
            debrisObject.GetComponent<SawKey>().m_pickedUpThisRun = true;
            return debrisObject;
        }
    }
}