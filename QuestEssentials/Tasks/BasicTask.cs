using QuestEssentials.Messages;

namespace QuestEssentials.Tasks
{
    class BasicTask : QuestTask
    {
        public string Trigger { get; set; }

        public override bool OnCheckProgress(StoryMessage message)
        {
            if (!this.IsCompleted() && this.Trigger == message.Trigger && this.IsWhenMatched())
            {
                this.IncrementCount(1);

                return true;
            }

            return false;
        }
    }
}
