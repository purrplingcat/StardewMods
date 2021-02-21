namespace QuestEssentials.Messages
{
    public class EarnMoneyMessage : StoryMessage
    {
        public int Amount { get; }

        public EarnMoneyMessage(int amount) : base("EarnMoney")
        {
            this.Amount = amount;
        }
    }
}
