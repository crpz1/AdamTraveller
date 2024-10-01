using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace AdamTraveller
{

    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class AdamTraveller : BaseUnityPlugin
    {
        public static AdamTraveller Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        private static AssetBundle audioBundle;
        public static AudioClip BiomeAudioClip;
        public static AudioClip DimensionAudioClip;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            audioBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "mymodbundle"));
            if (audioBundle == null) {
                Logger.LogError("Failed to load custom assets."); 
                return;
            }

            BiomeAudioClip = audioBundle.LoadAsset<AudioClip>("Assets/Audio/biome.ogg");
            DimensionAudioClip = audioBundle.LoadAsset<AudioClip>("Assets/Audio/dimension.ogg");

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            Harmony.PatchAll();

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }

    }

}