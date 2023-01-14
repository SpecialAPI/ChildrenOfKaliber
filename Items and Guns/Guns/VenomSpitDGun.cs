﻿using UnityEngine;
using Gungeon;
using Alexandria.ItemAPI;
namespace Items
{
    class VenomSpitDGun : GunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("venom_spit_drone_gun", "venom_spit_drone_gun");
            Game.Items.Rename("outdated_gun_mods:venom_spit_drone_gun", "cel:venom_spit_drone_gun");
            VenomSpitDGun @object = gun.gameObject.AddComponent<VenomSpitDGun>();
            gun.SetShortDescription("fswwawdw");
            gun.SetLongDescription("wsdfeesadwdasdwda.");
            gun.SetupSprite(null, "venom_spit_drone_gun_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.SetAnimationFPS(gun.reloadAnimation, 10);
            gun.AddProjectileModuleFrom("future_assault_rifle");
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = .6f;
            gun.DefaultModule.angleVariance = 4f;
            gun.DefaultModule.cooldownTime = .11f;
            gun.DefaultModule.numberOfShotsInClip = 25;
            gun.gunSwitchGroup = (PickupObjectDatabase.GetById(229) as Gun).gunSwitchGroup;
            Gun gun2 = PickupObjectDatabase.GetById(151) as Gun;
            gun.muzzleFlashEffects = gun2.muzzleFlashEffects;
            gun.SetBaseMaxAmmo(500);
            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            gun.encounterTrackable.EncounterGuid = "mama, uwu.";
            gun.sprite.IsPerpendicular = true;
            gun.gunClass = GunClass.FULLAUTO;
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.transform.parent = gun.barrelOffset;
            projectile.baseData.damage *= 0;
            projectile.baseData.speed *= 1f;
            projectile.baseData.force *= 0f;
            projectile.HasDefaultTint = true;
            projectile.DefaultTintColor = new Color(78 / 90f, 5 / 90f, 120 / 90f);
            projectile.AppliesPoison = true;
            projectile.healthEffect = Library.Venom;
            ETGMod.Databases.Items.Add(gun.GetComponent<PickupObject>());


        }

        private int i;

        private bool HasReloaded;

        public override void Update()
        {
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

        public VenomSpitDGun()
        {

        }
    }
}

