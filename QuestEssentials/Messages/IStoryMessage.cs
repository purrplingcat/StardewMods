namespace QuestEssentials.Messages
{
    public interface IStoryMessage
    {
        string Name { get; }
        string Trigger { get; }

        string ToString();
    }
}