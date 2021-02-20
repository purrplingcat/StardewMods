using QuestEssentials.Quests.Story;
using QuestFramework.Messages;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Messages
{
    public class TalkMessage : StoryMessage, ITalkMessage
    {
        public Farmer Farmer { get; }

        public NPC Npc { get; }

        public TalkMessage(Farmer farmer, NPC speaker) : base("Talk")
        {
            this.Farmer = farmer;
            this.Npc = speaker;
        }
    }
}
