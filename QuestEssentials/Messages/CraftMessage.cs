using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Messages
{
    public class CraftMessage : StoryMessage
    {
        public Item Item { get; }
        public bool Cooking { get; }

        public CraftMessage(Item item, bool cooking) : base("Craft")
        {
            this.Item = item;
            this.Cooking = cooking;
        }
    }
}
