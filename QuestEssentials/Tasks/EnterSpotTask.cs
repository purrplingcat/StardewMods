using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using QuestEssentials.Framework;
using QuestEssentials.Messages;
using QuestEssentials.Quests.Messages;

namespace QuestEssentials.Tasks
{
    class EnterSpotTask : StoryQuestTask
    {
        public struct WalkTaskData
        {
            [JsonConverter(typeof(PointConverter))]
            public Point? Tile { get; set; }
            [JsonConverter(typeof(RectangleConverter))]
            public Rectangle? Area { get; set; }
            public string Location { get; set; }
        }

        public WalkTaskData Data { get; set; }

        public override bool OnCheckProgress(StoryMessage message)
        {
            if (message.Trigger != "PlayerMoved" || !this.IsWhenMatched())
                return false;

            if (message is PlayerMovedMessage movedMessage)
            {
                if (movedMessage.Location.Name != this.Data.Location)
                    return false;

                if (this.Data.Tile.HasValue && this.Data.Tile.Value == movedMessage.TilePosition)
                {
                    this.IncrementCount(1);
                    return true;
                }

                if (this.Data.Area.HasValue && this.Data.Area.Value.Contains((int)movedMessage.Position.X, (int)movedMessage.Position.Y))
                {
                    this.IncrementCount(1);
                    return true;
                }
            }

            return false;
        }
    }
}
