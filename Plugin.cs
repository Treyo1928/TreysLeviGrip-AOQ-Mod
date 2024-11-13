using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace TreysLeviGrip
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string pluginGuid = "aoq.treyslevigrip";
        public const string pluginName = "Treys Levi Grip";
        public const string pluginVersion = "1.0.0";
        internal static BepInEx.Logging.ManualLogSource PluginLogger;

        public static bool leviGrip = false;
        private static string rotationAxis = "Z";
        public static Vector3 offset = new Vector3(82, 0, 180);
        public static bool flipAnimation = false;
        public static float flipStartTime;
        public static float flipDuration = 0.25f;

        public void Awake()
        {
            PluginLogger = Logger;

            Logger.LogInfo("Plugin TreysLeviGrip is loaded!");

            Harmony harmony = new Harmony("TreysLeviGrip");
            harmony.PatchAll();

            Logger.LogInfo("Harmony patches applied.");
        }

        public void Update()
        {
            if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) && !flipAnimation)
            {
                flipAnimation = true;
                leviGrip = !leviGrip;
                flipStartTime = Time.time;
            }
            //    if (Input.GetKey(KeyCode.Z)) rotationAxis = "Z";
            //    if (Input.GetKey(KeyCode.X)) rotationAxis = "X";
            //    if (Input.GetKey(KeyCode.Y)) rotationAxis = "Y";
            //    if (Input.GetKey(KeyCode.UpArrow))
            //    {
            //        switch (rotationAxis)
            //        {
            //            case "X":
            //                offset = offset + new Vector3(1, 0, 0);
            //                break;
            //            case "Y":
            //                offset = offset + new Vector3(0, 1, 0);
            //                break;
            //            case "Z":
            //                offset = offset + new Vector3(0, 0, 1);
            //                break;
            //        }
            //    }

            //    if (Input.GetKey(KeyCode.DownArrow))
            //    {
            //        switch (rotationAxis)
            //        {
            //            case "X":
            //                offset = offset - new Vector3(1, 0, 0);
            //                break;
            //            case "Y":
            //                offset = offset - new Vector3(0, 1, 0);
            //                break;
            //            case "Z":
            //                offset = offset - new Vector3(0, 0, 1);
            //                break;
            //        }
            //    }

            //    Logger.LogInfo($"Offset: {offset}");
        }
    }
}