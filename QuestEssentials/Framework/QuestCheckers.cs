using QuestEssentials.Messages;
using QuestEssentials.Quests;
using QuestFramework.Api;
using QuestFramework.Extensions;
using QuestFramework.Quests;
using StardewValley;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SObject = StardewValley.Object;

namespace QuestEssentials.Framework
{
    internal static class QuestCheckers
    {
        private static IManagedQuestApi QuestApi => QuestEssentialsMod.QuestApi;

        private static IEnumerable<Quest> GetQuestsForCheck<TQuest>() where TQuest : CustomQuest
        {
            return Game1.player
                .questLog
                .Where(q => !q.completed.Value && q.AsManagedQuest() is TQuest);
        }

        public static void CheckEarnQuests(int earnedMoney)
        {
            if (earnedMoney < 0)
                return;

            QuestApi.CheckForQuestComplete(new EarnMoneyMessage(earnedMoney));
        }

        public static void CheckSellQuests(Item itemToSell, int price, bool ship = false)
        {
            QuestApi.CheckForQuestComplete(new SellItemMessage(itemToSell, price, ship));
        }

        public static void CheckTalkQuests(Farmer farmer, NPC currentSpeaker)
        {
            QuestApi.CheckForQuestComplete(new NpcSpeakMessage(farmer, currentSpeaker));
        }
    }
}
