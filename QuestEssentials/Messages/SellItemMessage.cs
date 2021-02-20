using QuestEssentials.Quests.Story;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Messages
{
    public class SellItemMessage : StoryMessage
    {
        public Item Item { get; }
        public int ItemValue { get; }
        public bool Ship { get; }

        public SellItemMessage(Item item, int itemValue, bool ship = false) : base("SellItem")
        {
            this.Item = item ?? throw new ArgumentNullException(nameof(item));
            this.ItemValue = itemValue;
            this.Ship = ship;
        }
    }
}
