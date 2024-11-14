using HarmonyLib;
using Photon.Pun;
using System;
using UnityEngine;

namespace TreysLeviGrip
{
    [HarmonyPatch(typeof(NetworkWeaponSwap), "Update")]
    class networkWeaponSwapPatch
    {
        private static float pressStartTime = 0f;
        private static bool pressingButton = false;
        private static bool doReturn = true;

        static bool Prefix(NetworkWeaponSwap __instance)
        {
            doReturn = true;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
            {
                pressStartTime = Time.time;
                pressingButton = true;
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
            {
                if (Time.time - pressStartTime > Plugin.rightHoldDuration && pressingButton)
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

        private static void swapWeapon(NetworkWeaponSwap __instance)
        {
            var rightSword = Traverse.Create(__instance).Field("rightSword").GetValue<GameObject>();
            var networkRightSword = Traverse.Create(__instance).Field("networkRightSword").GetValue<NetworkRightSword>();
            if (!__instance.photonView.IsMine)
            {
                return;
            }
            if (rightSword.activeSelf)
            {
                networkRightSword.SwordDisabled();
            }
            __instance.photonView.RPC("SwapWeapon", RpcTarget.All, Array.Empty<object>());
        }
    }
}
