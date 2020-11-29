﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Gungeon;
namespace Items
{
    class AK141 : AdvancedGunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("AK-141", "ak_141");
            Game.Items.Rename("outdated_gun_mods:ak-141", "cel:ak-141");
            gun.gameObject.AddComponent<AK141>();
            gun.SetShortDescription("What The Hell?");
            gun.SetLongDescription("How does it even work? DOES it work? How are you supposed to reload it?");
            gun.SetupSprite(null, "ak_141_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 9);
            gun.SetAnimationFPS(gun.reloadAnimation, 10);
            gun.AddProjectileModuleFrom("ak-47", true, false);
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET;
            gun.DefaultModule.ammoCost = 3;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 1.5f;
            gun.DefaultModule.angleVariance = 4f;
            gun.DefaultModule.cooldownTime = .06f;
            gun.DefaultModule.numberOfShotsInClip = 90;
            gun.gunSwitchGroup = (PickupObjectDatabase.GetById(15) as Gun).gunSwitchGroup;
            gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(15) as Gun).muzzleFlashEffects;
            gun.SetBaseMaxAmmo(1500);
            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            gun.encounterTrackable.EncounterGuid = "why am i doing this";
            gun.sprite.IsPerpendicular = true;
            gun.barrelOffset.transform.localPosition = new Vector3(2.25f, 0.3125f, 0f);
            gun.gunClass = GunClass.FULLAUTO;
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.transform.parent = gun.barrelOffset;
            projectile.baseData.damage *= 1f;
            projectile.baseData.speed *= 1f;
            projectile.baseData.force *= 1f;
            ETGMod.Databases.Items.Add(gun, null, "ANY");
        }
        private bool HasReloaded;


        protected override void Update()
        {
            base.Update();
            if (gun.CurrentOwner)
            {

                if (gun.PreventNormalFireAudio)
                {
                    this.gun.PreventNormalFireAudio = true;
                }
                if (!gun.IsReloading && !HasReloaded)
                {
                    this.HasReloaded = true;
                }
            }
        }
        private float Tracker = 0;
        protected override void OnPickedUpByPlayer(PlayerController player)
        {
            base.OnPickedUpByPlayer(player);
            player.OnKilledEnemy += this.Transforming;
            Gun gun = ETGMod.Databases.Items["cel:ak_94"] as Gun;
            if (player.HasGun(gun.PickupObjectId))
            {
                player.inventory.DestroyGun(gun);
            }
        }
        protected override void OnPostDroppedByPlayer(PlayerController player)
        {
            base.OnPostDroppedByPlayer(player);
            player.OnKilledEnemy -= this.Transforming;
        }
        private void Transforming(PlayerController player)
        {
            if(player.CurrentGun == this)
            {
                this.Tracker++;
                if (Tracker == 45)
                {
                    Gun gun = ETGMod.Databases.Items["cel:ak_188"] as Gun;
                    player.inventory.AddGunToInventory(gun, true);
                    player.inventory.DestroyGun(ETGMod.Databases.Items["cel:ak_141"] as Gun);
                }
            }
        }

        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);
            }
        }

        private float revAngle = 180;
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            base.OnPostFired(player, gun);
            float v1 = UnityEngine.Random.Range(-4f, 4f);
            float v2 = UnityEngine.Random.Range(-4f, 4f);
            Projectile projectile2 = ((Gun)ETGMod.Databases.Items[15]).DefaultModule.projectiles[0];
            GameObject gameObject2 = SpawnManager.SpawnProjectile(projectile2.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : base.Owner.CurrentGun.CurrentAngle + revAngle + v1), true);
            Projectile component2 = gameObject2.GetComponent<Projectile>();
            if (component2 != null)
            {
                component2.Owner = base.Owner;
                component2.Shooter = base.Owner.specRigidbody;
                component2.baseData.speed *= player.stats.GetStatValue(PlayerStats.StatType.ProjectileSpeed);
                component2.baseData.force *= player.stats.GetStatValue(PlayerStats.StatType.KnockbackMultiplier);
                component2.baseData.damage *= player.stats.GetStatValue(PlayerStats.StatType.Damage);
            }
            
            Projectile projectile3 = ((Gun)ETGMod.Databases.Items[15]).DefaultModule.projectiles[0];
            GameObject gameObject3 = SpawnManager.SpawnProjectile(projectile3.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : base.Owner.CurrentGun.CurrentAngle + 270 + v2), true);
            Projectile component3 = gameObject3.GetComponent<Projectile>();
            if (component3 != null)
            {
                component3.Owner = base.Owner;
                component3.Shooter = base.Owner.specRigidbody;
                component3.baseData.speed *= player.stats.GetStatValue(PlayerStats.StatType.ProjectileSpeed);
                component3.baseData.force *= player.stats.GetStatValue(PlayerStats.StatType.KnockbackMultiplier);
                component3.baseData.damage *= player.stats.GetStatValue(PlayerStats.StatType.Damage);
            }
            
        }

        public AK141()
        {

        }
    }
}
