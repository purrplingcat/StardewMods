using PurrplingCore.Dialogues;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;
using SObject = StardewValley.Object;

namespace CustomGiftDialogue
{
    internal static class GiftDialogueHelper
    {
        /// <summary>
        /// Try find a gift reaction dialogue text for gifted object to an NPC
        /// </summary>
        /// <param name="npc">NPC had recieved a gift</param>
        /// <param name="obj">Gifted item recieved by this NPC</param>
        /// <param name="dialogue">A reaction dialogue text for this gifted item to this NPC</param>
        /// <returns>True if any gift reaction dialogue was found, otherwise false</returns>
        public static bool FetchGiftReaction(NPC npc, SObject obj, out string dialogue)
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

        /// <summary>
        /// Cancels current spoken dialogue for an NPC. 
        /// DialogueBox for this dialogue will not shown.
        /// </summary>
        /// <param name="speaker">The NPC speaker</param>
        public static void CancelCurrentDialogue(NPC speaker)
        {
            if (Game1.activeClickableMenu is DialogueBox dialogueBox && Game1.currentSpeaker == speaker)
            {
                // We wants override the default gift reaction dialogue
                if (speaker.CurrentDialogue.Count > 0
                    && GetDialogueBoxDialogue(dialogueBox) == speaker.CurrentDialogue.Peek())
                {
                    speaker.CurrentDialogue.Pop();
                }

                Game1.dialogueUp = false;
                Game1.currentSpeaker = null;
                Game1.activeClickableMenu = null;
            }
        }

        private static Dialogue GetDialogueBoxDialogue(DialogueBox dialogueBox)
        {
            return CustomGiftDialogueMod.Reflection
                .GetField<Dialogue>(dialogueBox, "characterDialogue")
                .GetValue();
        }
    }
}
