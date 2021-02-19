using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Quests.Story.Tasks
{
    class BasicTask : StoryQuestTask
    {
        public string Trigger { get; set; }

        public override void OnCompletionCheck(StoryMessage message)
        {
            if (!this.IsCompleted() && this.Trigger == message.Trigger && this.IsWhenMatched())
            {
                this.Increment(1);
            }
        }
    }
}
