using Newtonsoft.Json;
using QuestEssentials.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Tasks
{
    public class SlayTask : StoryQuestTask<SlayTask.SlayData>
    {
        public struct SlayData
        {
            public string TargetName { get; set; }
        }

        [JsonIgnore]
        public List<string> targetNames;

        public override void Load()
        {
            base.Load();

            if (this.targetNames == null)
            {
                this.targetNames = new List<string>();

                if (this.Data.TargetName != null)
                {
                    foreach (string t in this.Data.TargetName.Split(','))
                        this.targetNames.Add(t.Trim());
                }
            }
        }

        public override bool OnCheckProgress(StoryMessage message)
        {
            if (this.IsCompleted())
                return false;

            if (message is VanillaCompletionMessage args && args.CompletionType == 4 && this.IsWhenMatched())
            {
                if (this.targetNames.Count == 0 && !string.IsNullOrEmpty(args.String))
                {
                    this.IncrementCount(1);

                    return true;
                }
                    
                foreach (string targetName in this.targetNames)
                {
                    if (args.String != null && args.String.Contains(targetName))
                    {
                        this.IncrementCount(1);

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
