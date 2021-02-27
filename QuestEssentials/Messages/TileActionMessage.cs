using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Messages
{
    class TileActionMessage : StoryMessage
    {
        public Farmer Farmer { get; }
        public GameLocation Location { get; }
        public Point TilePosition { get; }

        public TileActionMessage(Farmer farmer, GameLocation location, Point tilePosition) : base("TileAction")
        {
            this.Farmer = farmer;
            this.Location = location;
            this.TilePosition = tilePosition;
        }
    }
}
