using QuestEssentials.Messages;

namespace QuestEssentials.Tasks
{
    class BasicTask : StoryQuestTask
    {
        public string Trigger { get; set; }

        public override bool OnCheckProgress(StoryMessage message)
        {
            if (!this.IsCompleted() && this.Trigger == message.Trigger && this.IsWhenMatched())
            {
                this.Increment(1);

                return true;
            }

            return false;
        }
    }
}
