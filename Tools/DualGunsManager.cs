﻿namespace Items
{
    class DualGunsManager
    {
        public static void AddDual()
        {
            /*DualWieldController PrimeSawController = ETGMod.Databases.Items["prime_saw"].gameObject.AddComponent<DualWieldController>();
            PrimeSawController.PartnerGunID = ETGMod.Databases.Items["prime_vice"].PickupObjectId;
            PrimeSawController.NoHands = true;

            DualWieldController PrimeViceController = ETGMod.Databases.Items["prime_vice"].gameObject.AddComponent<DualWieldController>();
            PrimeViceController.PartnerGunID = ETGMod.Databases.Items["prime_saw"].PickupObjectId;
            PrimeViceController.NoHands = true;
            */

            DualWieldController RussianRevolverController = ETGMod.Databases.Items["russian_revolver"].gameObject.AddComponent<DualWieldController>();
            RussianRevolverController.PartnerGunID = 2;

            DualWieldController TommyGunController = (PickupObjectDatabase.GetById(2) as Gun).gameObject.AddComponent<DualWieldController>();
            TommyGunController.PartnerGunID = ETGMod.Databases.Items["russian_revolver"].PickupObjectId;
            /*
           DualWieldController MiniLeftHandController = PickupObjectDatabase.GetById(MiniUberbotHand.lesserLeftHand).gameObject.AddComponent<DualWieldController>();
           MiniLeftHandController.PartnerGunID = MiniUberbotHand2.greaterRight;

           DualWieldController MiniRightHandController = PickupObjectDatabase.GetById(MiniUberbotHand2.greaterRight).gameObject.AddComponent<DualWieldController>();
           MiniRightHandController.PartnerGunID = MiniUberbotHand.lesserLeftHand;
           */
        }
    }
}
