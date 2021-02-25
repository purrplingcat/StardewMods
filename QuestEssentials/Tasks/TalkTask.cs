using QuestEssentials.Framework;
using QuestEssentials.Framework.Factories;
using QuestEssentials.Messages;
using QuestFramework.Messages;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Tasks
{
    class TalkTask : StoryQuestTask<TalkTask.TalkData>
    {
        public struct TalkData
        {
            public string NpcName { get; set; }
            public string DialogueText { get; set; }
            public string StartEvent { get; set; }
            public string ReceiveItems { get; set; }
        }

        /// <summary>
        /// Internal message internally sent by <see cref="DoAdjust(object)"/>
        /// </summary>
        private class TalkRequest : StoryMessage
        {
            public Farmer who;
            public NPC speaker;

            public TalkRequest(Farmer who, NPC speaker) : base("TalkRequest")
            {
                this.who = who;
                this.speaker = speaker;
            }
        }

        public override bool OnCheckProgress(StoryMessage message)
        {
            if (message is TalkRequest talkRequest)
            {
                if (talkRequest.speaker.Name == this.Data.NpcName)
                {
                    var dialogue = new Dialogue(this.Data.DialogueText, talkRequest.speaker)
                    {
                        onFinish = this.OnDialogueSpoken
                    };

                    talkRequest.speaker.CurrentDialogue.Push(dialogue);
                    Game1.drawDialogue(talkRequest.speaker);
                    this.IncrementCount(this.Goal);
                }
            }

            return false;
        }

        private void OnDialogueSpoken()
        {
            if (this.Data.ReceiveItems != null)
            {
                this.AddItemsToInvertory();
            }

            if (this.Data.StartEvent != null)
            {
                this.StartEvent();
            }
        }

        private void AddItemsToInvertory()
        {
            string[] itemDescriptions = this.Data.ReceiveItems.Split(',');
            List<Item> items = itemDescriptions.Select(d => ItemFactory.Create(d))
                .Where(i => i != null)
                .ToList();

            Game1.activeClickableMenu?.exitThisMenu(playSound: false);
            Game1.player.addItemsByMenuIfNecessary(items);
        }

        private void StartEvent()
        {
            var eventStartAction = new DelayedAction(1, () => Game1.player.currentLocation.StartEventFrom(this.Data.StartEvent)) 
            { 
                waitUntilMenusGone = true 
            };

            Game1.delayedActions.Add(eventStartAction);
        }

        public override void DoAdjust(object toAdjust)
        {
            if (toAdjust is ITalkMessage talkAdjust && !Game1.dialogueUp)
            {
                // Quest Framework sends an object to adjust when Farmer requests talk with NPC via NPC.checkAction
                // We catch it and transfer as TalkRequest and call OnCheckProgress with the request as argument
                if (this.IsRegistered() && !this.IsCompleted())
                {
                    this.OnCheckProgress(new TalkRequest(talkAdjust.Farmer, talkAdjust.Npc));
                }
            }
        }

        public override bool ShouldShowProgress()
        {
            return false;
        }
    }
}
