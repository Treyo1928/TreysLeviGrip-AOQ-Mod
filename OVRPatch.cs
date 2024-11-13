using HarmonyLib;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TreysLeviGrip
{
    [HarmonyPatch(typeof(OVRCameraRig), "UpdateAnchors")]
    class OVRPatch
    {
        static void Postfix(OVRCameraRig __instance)
        {
            if (Plugin.leviGrip && !Plugin.flipAnimation)
            {
                // Apply the offset to both hands, rotating them in the same direction
                __instance.leftHandAnchor.Rotate(Plugin.offset);
                __instance.rightHandAnchor.Rotate(Plugin.offset);
            }
            else if (Plugin.flipAnimation)
            {
                Plugin.PluginLogger.LogInfo("Flipping");

                // Interpolate rotation for the left hand
                Vector3 startRotation = Plugin.leviGrip ? Vector3.zero : Plugin.offset;
                Vector3 endRotation = Plugin.leviGrip ? Plugin.offset : Vector3.zero;
                Vector3 rotationOffset = Vector3.Lerp(startRotation, endRotation, (Time.time - Plugin.flipStartTime) / Plugin.flipDuration);
                __instance.leftHandAnchor.Rotate(rotationOffset);
                
                startRotation = Plugin.leviGrip ? Vector3.zero : Plugin.offset + new Vector3(0, 0, -360);
                endRotation = Plugin.leviGrip ? Plugin.offset + new Vector3(0, 0, -360) : Vector3.zero;
                rotationOffset = Vector3.Lerp(startRotation, endRotation, (Time.time - Plugin.flipStartTime) / Plugin.flipDuration);
                __instance.rightHandAnchor.Rotate(rotationOffset);

                // End flip animation after specified duration
                if (Time.time - Plugin.flipStartTime > Plugin.flipDuration) Plugin.flipAnimation = false;
            }
        }

    }
}
