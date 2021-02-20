using QuestEssentials.Quests.Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
