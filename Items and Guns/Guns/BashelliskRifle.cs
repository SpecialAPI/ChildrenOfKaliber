﻿using Gungeon;
using Alexandria.ItemAPI;
using UnityEngine;

namespace Items
{
    class BashelliskRifle : GunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Bashellisk Rifle", "bashellisk_rifle");
            Game.Items.Rename("outdated_gun_mods:bashellisk_rifle", "ck:bashellisk_rifle");
            gun.gameObject.AddComponent<BashelliskRifle>();
            gun.SetShortDescription("Deadly Deadly Gun");
            gun.SetLongDescription("Shoots toxic shots which can somtimes afflict enemies with a powerful venom.\n\nInfused with the fang of a Bashellisk, this gun's bullets are empowered with an even deadlier power.");

            gun.SetupSprite(null, "bashellisk_rifle_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.SetAnimationFPS(gun.reloadAnimation, 10);
            gun.AddProjectileModuleFrom("future_assault_rifle");
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.CUSTOM;
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = .6f;
            gun.DefaultModule.customAmmoType = "poison_blob";
            gun.DefaultModule.angleVariance = 4f;
            gun.DefaultModule.cooldownTime = .11f;
            gun.DefaultModule.numberOfShotsInClip = 25;
            gun.gunSwitchGroup = (PickupObjectDatabase.GetById(229) as Gun).gunSwitchGroup;
            Gun gun2 = PickupObjectDatabase.GetById(334) as Gun;
            gun.muzzleFlashEffects = gun2.muzzleFlashEffects;
            gun.SetBaseMaxAmmo(500);
            gun.barrelOffset.transform.localPosition = new Vector3(1.4f, .4f, 0f);
            gun.quality = PickupObject.ItemQuality.S;
            gun.encounterTrackable.EncounterGuid = "Venom Rifle.";
            gun.sprite.IsPerpendicular = true;
            gun.gunClass = GunClass.FULLAUTO;
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.transform.parent = gun.barrelOffset;
            projectile.baseData.damage *= 1;
            projectile.baseData.speed *= 1f;
            projectile.baseData.force *= 1f;
            projectile.PoisonApplyChance = .25f;
            projectile.AppliesPoison = true;
            projectile.healthEffect = Library.Venom;
            projectile.DefaultTintColor = new Color(78 / 90f, 5 / 90f, 120 / 90f);
            projectile.HasDefaultTint = true;
            ETGMod.Databases.Items.Add(gun.GetComponent<PickupObject>());
            gun.AddToSubShop(ItemBuilder.ShopType.Goopton);
        }
        

        
       

        private bool HasReloaded;
        
        public override void Update()
        {
            if (gun.CurrentOwner)
            {

                
                if (!gun.IsReloading && !HasReloaded)
                {
                    this.HasReloaded = true;
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
        

        public override void OnPostFired(PlayerController player, Gun gun)
        {



        }



        public BashelliskRifle()
        {

        }
    }
}
