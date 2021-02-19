using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Quests.Story
{
    public class StoryMessage
    {
        public StoryMessage(string trigger)
        {
            this.Trigger = trigger;
        }

        public StoryMessage(string trigger, object data)
        {
            this.Trigger = trigger;
            this.Data = JsonConvert.SerializeObject(data);
        }

        public string Trigger { get; }
        public string Data { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
