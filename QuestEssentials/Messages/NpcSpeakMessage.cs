using QuestFramework.Messages;
using StardewValley;

namespace QuestEssentials.Messages
{
    public class NpcSpeakMessage : StoryMessage, ITalkMessage
    {
        public Farmer Farmer { get; }

        public NPC Npc { get; }

        public NpcSpeakMessage(Farmer farmer, NPC speaker) : base("NpcSpeak")
        {
            this.Farmer = farmer;
            this.Npc = speaker;
        }
    }
}
