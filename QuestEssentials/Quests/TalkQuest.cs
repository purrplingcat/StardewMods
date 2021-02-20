﻿using QuestFramework.Extensions;
using QuestFramework.Messages;
using QuestFramework.Quests;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Quests
{
    class TalkQuest : CustomQuest, IQuestInfoUpdater, ITriggerLoader
    {
        public string TalkTo { get; set; }

        public override void OnCompletionCheck(object completionMessage)
        {
            if (completionMessage is ITalkMessage talkMessage)
            {
                if (!this.IsRelevantMessage(talkMessage))
                    return;

                if (Game1.activeClickableMenu is DialogueBox && Game1.currentSpeaker == talkMessage.Npc)
                {
                    this.Complete();
                }
            }

            base.OnCompletionCheck(completionMessage);
        }

        private bool IsRelevantMessage(ITalkMessage talkMessage)
        {
            return this.TalkTo != null && talkMessage.Npc != null && talkMessage.Npc.isVillager() && this.TalkTo == talkMessage.Npc.Name;
        }

        public override void OnAdjust(object adjustMessage)
        {
            if (adjustMessage is ITalkMessage talkMessage)
            {
                if (!this.IsRelevantMessage(talkMessage))
                    return;

                if (!string.IsNullOrEmpty(this.ReactionText))
                {
                    talkMessage.Npc.CurrentDialogue.Push(new Dialogue(this.ReactionText, talkMessage.Npc));
                    Game1.drawDialogue(talkMessage.Npc);
                }
            }

            base.OnAdjust(adjustMessage);
        }

        public void LoadTrigger(string triggerData)
        {
            this.TalkTo = triggerData;
        }

        public void UpdateDescription(IQuestInfo questData, ref string description)
        {
        }

        public void UpdateObjective(IQuestInfo questData, ref string objective)
        {
        }

        public void UpdateTitle(IQuestInfo questData, ref string title)
        {
        }
    }
}
