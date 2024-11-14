using HarmonyLib;
using UnityEngine;

namespace TreysLeviGrip
{
    [HarmonyPatch(typeof(WeaponSwap), "Update")]
    class weaponSwapPatch
    {
        private static float pressStartTime = 0f;
        private static bool pressingButton = false;
        private static bool doReturn = true;

        static bool Prefix(WeaponSwap __instance)
        {
            doReturn = true;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
            {
                pressStartTime = Time.time;
                pressingButton = true;
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
            {
                if(Time.time - pressStartTime > Plugin.rightHoldDuration && pressingButton)
                {
                    Plugin.PluginLogger.LogInfo("Swapping Weapon");
                    pressingButton = false;
                    swapWeapon(__instance);
                }
            }
            else
            {
                if (pressingButton)
                {
                    pressingButton = false;
                    Plugin.FlipRight();
                    Plugin.PluginLogger.LogInfo("Flipping Handle");
                }
            }
            return false;
        }

        static void swapWeapon(WeaponSwap __instance)
        {
            var timerCanvas = Traverse.Create(__instance).Field("timerCanvas").GetValue<GameObject>();
            var player = Traverse.Create(__instance).Field("player").GetValue<GameObject>();

            if (__instance.rightSword.activeSelf)
            {
                __instance.rightSword.SetActive(false);
                timerCanvas.SetActive(false);
                __instance.flareGun.SetActive(true);
                player.GetComponent<Salute>().saluteRight1 = false;
                player.GetComponent<Salute>().saluteRight2 = false;
            }
            else
            {
                __instance.rightSword.SetActive(true);
                timerCanvas.SetActive(true);
                __instance.flareGun.SetActive(false);
            }
        }
    }
}
