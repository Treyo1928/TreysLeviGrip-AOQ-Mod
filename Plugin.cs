using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace TreysLeviGrip
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string pluginGuid = "aoq.treyslevigrip";
        public const string pluginName = "Treys Levi Grip";
        public const string pluginVersion = "1.0.0";
        internal static BepInEx.Logging.ManualLogSource PluginLogger;

        public static bool leftLeviGrip = false;
        public static bool rightLeviGrip = false;

        //private static string rotationAxis = "Z";
        
        public static Vector3 offset = new Vector3(82, 0, 180);
        
        public static bool leftFlipAnimation = false;
        public static float leftFlipStartTime;

        public static bool rightFlipAnimation = false;
        public static float rightFlipStartTime;

        public static float flipDuration = 0.25f;

        public static float rightHoldDuration = 0.2f;

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
            if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) && !leftFlipAnimation)
            {
                leftFlipAnimation = true;
                leftLeviGrip = !leftLeviGrip;
                leftFlipStartTime = Time.time;
            }
        }
        public static void FlipRight()
        {
            rightFlipAnimation = true;
            rightLeviGrip = !rightLeviGrip;
            rightFlipStartTime = Time.time;
        }
    }
}