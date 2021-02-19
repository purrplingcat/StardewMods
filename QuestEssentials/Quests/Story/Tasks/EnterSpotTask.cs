using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using QuestEssentials.Framework;
using QuestEssentials.Quests.Story.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Quests.Story.Tasks
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

        public override void OnCompletionCheck(StoryMessage message)
        {
            if (message.Trigger != "PlayerMoved" || !this.IsWhenMatched())
                return;

            if (message is PlayerMovedMessage movedMessage)
            {
                if (movedMessage.Location.Name != this.Data.Location)
                    return;

                if (this.Data.Tile.HasValue && this.Data.Tile.Value == movedMessage.TilePosition)
                {
                    this.Increment(1);
                    return;
                }

                if (this.Data.Area.HasValue && this.Data.Area.Value.Contains((int)movedMessage.Position.X, (int)movedMessage.Position.Y))
                {
                    this.Increment(1);
                    return;
                }
            }
        }
    }
}
