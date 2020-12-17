using Harmony;
using PurrplingCore.Dialogues;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using StardewValley;
using System;
using System.Collections.Generic;
using SObject = StardewValley.Object;

namespace CustomGiftDialogue
{
    /// <summary>The mod entry point.</summary>
    public class CustomGiftDialogueMod : Mod
    {
        internal static IMonitor ModMonitor { get; private set; }

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(typeof(NPC), nameof(NPC.receiveGift)),
                postfix: new HarmonyMethod(this.GetType(), nameof(PATCH__After_receiveGift))
            );
        }

        private static void PATCH__After_receiveGift(NPC __instance, SObject o)
        {
            try
            {
                string dialogueKey = $"GiftReaction_{o.Name.Replace(' ', '_')}";

                if (DialogueHelper.GetRawDialogue(__instance.Dialogue, dialogueKey, out KeyValuePair<string, string> reaction))
                {
                    Game1.drawDialogue(__instance, reaction.Value);
                }
            }
            catch (Exception ex)
            {
                ModMonitor.Log($"An error occurred during handle custom gift reaction dialogue: {ex.Message}", LogLevel.Error);
                ModMonitor.Log(ex.ToString());
            }
        }
    }
}