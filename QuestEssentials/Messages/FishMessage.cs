using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Messages
{
    public class FishMessage : StoryMessage
    {
        public Farmer Farmer { get; }
        public Item Fish { get; }

        public FishMessage(Farmer farmer, Item fish) : base("Fish")
        {
            this.Farmer = farmer;
            this.Fish = fish;
        }
    }
}
