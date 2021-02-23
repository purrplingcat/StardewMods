using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using QuestEssentials.Framework;
using QuestEssentials.Messages;
using QuestEssentials.Quests.Messages;
using StardewValley;
using System;
using System.Linq;

namespace QuestEssentials.Tasks
{
    class EnterSpotTask : StoryQuestTask<EnterSpotTask.EnterSpotData>
    {
        public struct EnterSpotData
        {
            [JsonConverter(typeof(PointConverter))]
            public Point? Tile { get; set; }
            [JsonConverter(typeof(RectangleConverter))]
            public Rectangle? Area { get; set; }
            public string Location { get; set; }
            public string EventOnComplete { get; set; }
        }

        protected override void OnTaskComplete()
        {
            if (this.Data.EventOnComplete == null)
                return;

            if (this.Data.Location == null || Game1.player.currentLocation.Name != this.Data.Location)
                return;

            string[] eventInfo = this.Data.EventOnComplete.Split(' ');
            int eventId = Convert.ToInt32(eventInfo[0]);
            string path = string.Join(" ", eventInfo.Skip(1));

            Game1.player.Halt();
            Game1.globalFadeToBlack(delegate
            {
                Game1.player.currentLocation.startEvent(new Event(Game1.content.LoadString(path), eventId));
            });
        }

        public override bool ShouldShowProgress()
        {
            return false;
        }

        public override bool OnCheckProgress(StoryMessage message)
        {
            if (message.Trigger != "PlayerMoved" || !this.IsWhenMatched() || this.IsCompleted())
                return false;

            if (message is PlayerMovedMessage movedMessage)
            {
                if (movedMessage.Location.Name != this.Data.Location)
                    return false;

                if (this.Data.Tile.HasValue && this.Data.Tile.Value == movedMessage.TilePosition)
                {
                    this.IncrementCount(this.Goal);
                    return true;
                }

                if (this.Data.Area.HasValue && this.Data.Area.Value.Contains((int)movedMessage.Position.X, (int)movedMessage.Position.Y))
                {
                    this.IncrementCount(this.Goal);
                    return true;
                }
            }

            return false;
        }
    }
}
