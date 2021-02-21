using QuestEssentials.Messages;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Tasks
{
    public class CollectTask : StoryQuestTask
    {
        public string AcceptedContextTags { get; set; }

        public override bool OnCheckProgress(StoryMessage message)
        {
            if (message is VanillaCompletionMessage completionArgs && completionArgs.CompletionType == Quest.type_resource && completionArgs.Item != null)
            {
                bool fail = false;

                if (this.AcceptedContextTags == null || !this.IsWhenMatched())
                    return false;

                foreach (string tags in this.AcceptedContextTags.Split(','))
                {
                    bool foundMatch = false;

                    foreach (string tag in tags.Split('/'))
                    {
                        if (completionArgs.Item.HasContextTag(tag.Trim()))
                        {
                            foundMatch = true;
                            break;
                        }
                    }

                    if (!foundMatch)
                    {
                        fail = true;
                    }
                }

                if (!fail)
                {
                    this.IncrementCount(completionArgs.Item.Stack);

                    return true;
                }
            }

            return false;
        }
    }
}
