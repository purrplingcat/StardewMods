using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Messages
{
    public class GiftMessage : StoryMessage
    {
        public Farmer Farmer { get; }
        public NPC Npc { get; }
        public Item Item { get; }

        public GiftMessage(Farmer farmer, NPC npc, Item item) : base("Gift")
        {
            this.Farmer = farmer;
            this.Npc = npc;
            this.Item = item;
        }
    }
}
