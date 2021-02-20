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

        public string Trigger { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
