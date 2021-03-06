﻿using StardewValley;
using System;

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
