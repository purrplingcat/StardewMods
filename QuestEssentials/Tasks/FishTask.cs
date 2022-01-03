using QuestEssentials.Framework;
using QuestEssentials.Messages;

namespace QuestEssentials.Tasks
{
    public class FishTask : QuestTask<FishTask.FishData>
    {
        public struct FishData
        {
            public string AcceptedContextTags { get; set; }
        }

        public override bool OnCheckProgress(IStoryMessage message)
        {
            if (this.IsCompleted())
                return false;

            if (message is FishMessage fishMessage)
            {
                if (fishMessage.Fish == null || this.Data.AcceptedContextTags == null || !this.IsWhenMatched())
                    return false;

                if (Helper.CheckItemContextTags(fishMessage.Fish, this.Data.AcceptedContextTags))
                {
                    this.IncrementCount(fishMessage.Fish.Stack);

                    return true;
                }
            }

            return false;
        }
    }
}
