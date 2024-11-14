using HarmonyLib;
using UnityEngine;

namespace TreysLeviGrip
{
    [HarmonyPatch(typeof(OVRCameraRig), "UpdateAnchors")]
    class OVRPatch
    {
        static void Postfix(OVRCameraRig __instance)
        {
            Vector3 startRotation;
            Vector3 endRotation;
            Vector3 rotationOffset;
            
            // Left Hand Flipping
            if (Plugin.leftLeviGrip && !Plugin.leftFlipAnimation) __instance.leftHandAnchor.Rotate(Plugin.offset);
            else if (Plugin.leftFlipAnimation)
            {
                //Plugin.PluginLogger.LogInfo("Flipping");

                startRotation = Plugin.leftLeviGrip ? Vector3.zero : Plugin.offset;
                endRotation = Plugin.leftLeviGrip ? Plugin.offset : Vector3.zero;
                rotationOffset = Vector3.Lerp(startRotation, endRotation, (Time.time - Plugin.leftFlipStartTime) / Plugin.flipDuration);
                __instance.leftHandAnchor.Rotate(rotationOffset);

                // End flip animation after specified duration
                if (Time.time - Plugin.leftFlipStartTime > Plugin.flipDuration) Plugin.leftFlipAnimation = false;
            }

            // Right Hand Flipping
            if (Plugin.rightLeviGrip && !Plugin.rightFlipAnimation) __instance.rightHandAnchor.Rotate(Plugin.offset);
            else if (Plugin.rightFlipAnimation)
            {
                //Plugin.PluginLogger.LogInfo("Flipping");

                startRotation = Plugin.rightLeviGrip ? Vector3.zero : Plugin.offset + new Vector3(0, 0, -360);
                endRotation = Plugin.rightLeviGrip ? Plugin.offset + new Vector3(0, 0, -360) : Vector3.zero;
                rotationOffset = Vector3.Lerp(startRotation, endRotation, (Time.time - Plugin.rightFlipStartTime) / Plugin.flipDuration);
                __instance.rightHandAnchor.Rotate(rotationOffset);

                // End flip animation after specified duration
                if (Time.time - Plugin.rightFlipStartTime > Plugin.flipDuration) Plugin.rightFlipAnimation = false;
            }
        }
    }
}
