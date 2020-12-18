using Harmony;
using PurrplingCore.Dialogues;
using StardewModdingAPI;
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
                if (FetchGiftReaction(__instance, o, out string giftDialogue))
                {
                    Game1.drawDialogue(__instance, giftDialogue);
                }
            }
            catch (Exception ex)
            {
                ModMonitor.Log($"An error occurred during handle custom gift reaction dialogue: {ex.Message}", LogLevel.Error);
                ModMonitor.Log(ex.ToString());
            }
        }

        /// <summary>
        /// Try find a gift reaction dialogue text for gifted object to an NPC
        /// </summary>
        /// <param name="npc">NPC had recieved a gift</param>
        /// <param name="obj">Gifted item recieved by this NPC</param>
        /// <param name="dialogue">A reaction dialogue text for this gifted item to this NPC</param>
        /// <returns>True if any gift reaction dialogue was found, otherwise false</returns>
        private static bool FetchGiftReaction(NPC npc, SObject obj, out string dialogue)
        {
            string[] possibleKeys = new string[] { $"GiftReaction_{obj.Name.Replace(' ', '_')}", $"GiftReactionCategory_{obj.Category}" };

            foreach (string dialogueKey in possibleKeys)
            {
                if (DialogueHelper.GetRawDialogue(npc.Dialogue, dialogueKey, out KeyValuePair<string, string> reaction))
                {
                    dialogue = reaction.Value;
                    return true;
                }
            }

            dialogue = "";
            return false;
        }
    }
}