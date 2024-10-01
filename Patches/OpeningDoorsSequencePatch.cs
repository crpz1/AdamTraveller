using HarmonyLib;

namespace AdamTraveller.Patches
{

    [HarmonyPatch(typeof(StartOfRound))]
    public class OpeningDoorsSequencePatch
    {

        [HarmonyPatch("openingDoorsSequence")]
        [HarmonyPostfix]
        private static void OpeningDoorsSequencePostfix(StartOfRound __instance)
        {
            HUDManager.Instance.UIAudio.PlayOneShot(AdamTraveller.DimensionAudioClip);
        }
    }
}