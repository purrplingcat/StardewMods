using Microsoft.Xna.Framework;
using QuestEssentials.Messages;
using StardewValley;

namespace QuestEssentials.Quests.Messages
{
    public class PlayerMovedMessage : StoryMessage
    {
        public PlayerMovedMessage(GameLocation location, Vector2 position, Point tilePosition, string trigger = "PlayerMoved") : base(trigger)
        {
            this.Location = location;
            this.Position = position;
            this.TilePosition = tilePosition;
        }

        public GameLocation Location { get; }
        public Vector2 Position { get; }
        public Point TilePosition { get; }
    }
}
