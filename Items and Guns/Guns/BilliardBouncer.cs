﻿using Gungeon;
using Alexandria.ItemAPI;
using UnityEngine;
namespace Items
{
    class BilliardBouncer : GunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Billiard Bouncer", "billiard_bouncer");
            Game.Items.Rename("outdated_gun_mods:billiard_bouncer", "ck:billiard_bouncer");
            gun.gameObject.AddComponent<BilliardBouncer>();
            gun.SetShortDescription("Solids Or Stripes?");
            gun.SetLongDescription("Shoots bouncing billiard balls that collide with other player projectiles. \n\nInvented by a gambler who enjoyed the sound of clinking billiard balls a little too much.");
            gun.SetupSprite(null, "billiard_bouncer_idle_001", 13);
            gun.SetAnimationFPS(gun.shootAnimation, 14);
            for (int i = 0; i < 3; i++)
            {
                GunExt.AddProjectileModuleFrom(gun, "38_special");
            }
            gun.barrelOffset.localPosition = new Vector3(1.1875f, 0.5625f, 0f);
            foreach (ProjectileModule projectileModule in gun.Volley.projectiles)
            {
                projectileModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
                Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(projectileModule.projectiles[0]);
                projectile.gameObject.SetActive(false);
                FakePrefab.MarkAsFakePrefab(projectile.gameObject);
                UnityEngine.Object.DontDestroyOnLoad(projectile);
                projectileModule.projectiles[0] = projectile;
                projectileModule.angleVariance = 10;
                projectile.transform.parent = gun.barrelOffset;
                gun.DefaultModule.projectiles[0] = projectile;
                projectile.baseData.damage *= 1;
                projectile.baseData.speed *= 1f;
                projectileModule.numberOfShotsInClip = 8;
                projectileModule.cooldownTime = .6f;
                BounceProjModifier bounce = projectile.gameObject.AddComponent<BounceProjModifier>();
                projectile.gameObject.AddComponent<BilliardBounceAdder>();
                bounce.damageMultiplierOnBounce = 1;
                bounce.numberOfBounces = 6;
                projectile.baseData.range *= 10f;
                projectileModule.ammoType = GameUIAmmoType.AmmoType.CUSTOM;
                projectileModule.customAmmoType = CustomClipAmmoTypeToolbox.AddCustomAmmoType("Billiard", "Items/Resources/CustomGunAmmoTypes/billiard_full", "Items/Resources/CustomGunAmmoTypes/billiard_empty");
                projectile.shouldRotate = true;
                projectile.SetProjectileSpriteRight("billiard_cue", 11, 11);
                bool flag = projectileModule == gun.DefaultModule;
                if (flag)
                {
                    projectileModule.ammoCost = 1;
                }
                else
                {
                    projectileModule.ammoCost = 0;
                }
            }
            gun.usesContinuousFireAnimation = false;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 1.5f;
            gun.Volley.UsesShotgunStyleVelocityRandomizer = true;
            gun.Volley.DecreaseFinalSpeedPercentMin = -10f;
            gun.Volley.IncreaseFinalSpeedPercentMax = 10f;
            gun.SetBaseMaxAmmo(150);
            gun.gunClass = GunClass.SILLY;   
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.quality = PickupObject.ItemQuality.B;
            gun.encounterTrackable.EncounterGuid = "Billiard Shotgun";
            gun.sprite.IsPerpendicular = true;
            gun.gunSwitchGroup = (PickupObjectDatabase.GetById(45) as Gun).gunSwitchGroup;
            ETGMod.Databases.Items.Add(gun.GetComponent<PickupObject>());
        }

        private bool HasReloaded;
        public override void PostProcessProjectile(Projectile projectile)
        {
            PlayerController player = gun.CurrentOwner as PlayerController;
            base.PostProcessProjectile(projectile);
            projectile.OnHitEnemy += HitEnemy;

        }
        private void HitEnemy(Projectile projectile, SpeculativeRigidbody enemy, bool killed)
        {
            PlayerController player = gun.CurrentOwner as PlayerController;
            BounceProjModifier bounce = projectile.gameObject.GetOrAddComponent<BounceProjModifier>();
            if(bounce.numberOfBounces > 1 && !player.HasPickupID(ETGMod.Databases.Items["Floop Bullets"].PickupObjectId))
            {
                bounce.numberOfBounces--;
                PierceProjModifier orAddComponent = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
                orAddComponent.penetratesBreakables = true;
                orAddComponent.penetration++;
                Vector2 dirVec = UnityEngine.Random.insideUnitCircle;
                projectile.SendInDirection(dirVec, false, true);
            }
        }
        
        public override void Update()
        {
            if (gun.CurrentOwner)
            {
                PlayerController player = gun.CurrentOwner as PlayerController;
                if (gun.PreventNormalFireAudio)
                {
                    this.gun.PreventNormalFireAudio = true;
                }
                if (!gun.IsReloading && !HasReloaded)
                {
                    this.HasReloaded = true;
                }
              //  if (player.HasPickupID(Library.LichEye) || player.HasPickupID(111) && IronStanceOn == false)
               // {
               //     gun.reloadTime = 1.2f;
                //    IronStanceOn = true;
              //  }
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
        
        public BilliardBouncer()
        {

        }
    }
}
