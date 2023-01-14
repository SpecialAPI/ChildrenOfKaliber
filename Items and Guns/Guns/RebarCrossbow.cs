﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Gungeon;
using Alexandria.ItemAPI;

namespace Items
{
    class RebarCrossbow : AdvancedGunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Rebar Crossbow", "rebar_crossbow");
            Game.Items.Rename("outdated_gun_mods:rebar_crossbow", "cel:rebar_crossbow");
            gun.gameObject.AddComponent<RebarCrossbow>();
            gun.SetShortDescription("Click");
            gun.SetLongDescription("Originally designed for basic home security, thousands of engineers have discovered obscure uses for the humble dispenser.");
            gun.SetupSprite(null, "rebar_crossbow_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.SetAnimationFPS(gun.reloadAnimation, 9);
            gun.AddProjectileModuleFrom("crossbow");
            gun.SetBaseMaxAmmo(180);
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 1.8f;
            gun.DefaultModule.cooldownTime = 0.25f;
            gun.gunClass = GunClass.FIRE;
            gun.DefaultModule.numberOfShotsInClip = 1;
            gun.quality = PickupObject.ItemQuality.B;
            Gun gun2 = PickupObjectDatabase.GetById(12) as Gun;
            gun.muzzleFlashEffects = gun2.muzzleFlashEffects;
            gun.DefaultModule.angleVariance = 0f;
            gun.encounterTrackable.EncounterGuid = "become stabbied by bolt of owie";
            gun.sprite.IsPerpendicular = true;
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage *= 1.5f;
            projectile.baseData.speed *= 2f;
            projectile.baseData.force *= 1f;
            projectile.transform.parent = gun.barrelOffset;
            projectile.SetProjectileSpriteRight("rebar_projectile", 11, 3);
            projectile.sprite.usesOverrideMaterial = true;
            projectile.sprite.renderer.material.shader = ChildrenOfKaliberModule.ModAssets.LoadAsset<Shader>("infernal_shader");
            projectile.gameObject.AddComponent<StickyProjectile>();
            projectile.AppliesFire = true;
            projectile.FireApplyChance = 1;
            projectile.fireEffect = (PickupObjectDatabase.GetById(295) as BulletStatusEffectItem).FireModifierEffect;
            ETGMod.Databases.Items.Add(gun.GetComponent<PickupObject>());
        }

        private bool HasReloaded;

        protected override void Update()
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
                AkSoundEngine.PostEvent("Play_WPN_crossbow_reload_01", base.gameObject);
            }
        }



        public override void OnPostFired(PlayerController player, Gun gun)
        {

            gun.PreventNormalFireAudio = true;
            AkSoundEngine.PostEvent("Play_WPN_crossbow_shot_01", gameObject);
        }


        public RebarCrossbow()
        {
        }

    }
    public class RebarExplosiveSynergyHandler : MonoBehaviour
    {
        private void Start()
        {
            proj = base.gameObject.GetComponent<Projectile>();
            if(proj != null)
            {
                if(player == null)
                {
                    player = proj.Owner as PlayerController;
                    if(player.PlayerHasActiveSynergy("Explosive Rebar"))
                    {
                        stickyProjectileData = proj.gameObject.GetComponent<StickyProjectile>();
                        stickyProjectileData.shouldExplode = true;
                        stickyProjectileData.explosionData = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
                        stickyProjectileData.explosionData.damageToPlayer = 0;
                        stickyProjectileData.explosionData.preventPlayerForce = true;
                        stickyProjectileData.maxLifeTime = 3f;
                        stickyProjectileData.explosionData.damage = proj.baseData.damage * 2f;
                    }
                }
            }
        }
        public PlayerController player;
        private Projectile proj;
        private StickyProjectile stickyProjectileData;
    }
}
