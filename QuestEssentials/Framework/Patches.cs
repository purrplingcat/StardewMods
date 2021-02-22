using QuestEssentials.Messages;
using QuestEssentials.Quests;
using QuestFramework.Extensions;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Linq;

namespace QuestEssentials.Framework
{
    internal static class Patches
    {
        public static void Before_receiveLeftClick(ShopMenu __instance, int x, int y, int ___sellPercentage)
        {
            try
            {
                if (Game1.activeClickableMenu == null)
                    return;

                if (__instance is ShopMenu)
                {
                    if (__instance.heldItem == null && __instance.onSell == null)
                    {
                        Item itemToSell = __instance.inventory.leftClick(x, y, null, false);

                        if (itemToSell != null)
                        {
                            int price = CalculatePrice(___sellPercentage, itemToSell);

                            QuestCheckers.CheckSellQuests(itemToSell, price);
                            __instance.inventory.leftClick(x, y, itemToSell, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuestEssentialsMod.ModMonitor
                    .Log(
                        $"Error in {nameof(Before_receiveLeftClick)} harmony patch: {ex}",
                        LogLevel.Error
                    );
            }
        }

        public static void Before_receiveRightClick(ShopMenu __instance, int x, int y, int ___sellPercentage)
        {
            try
            {
                if (Game1.activeClickableMenu == null)
                    return;

                if (__instance is ShopMenu)
                {
                    if (__instance.heldItem == null && __instance.onSell == null)
                    {
                        Item itemToSell = __instance.inventory.rightClick(x, y, null, false, false);

                        if (itemToSell != null)
                        {
                            int price = CalculatePrice(___sellPercentage, itemToSell);

                            QuestCheckers.CheckSellQuests(itemToSell, price);

                            if (itemToSell.Stack == 1)
                                __instance.inventory.leftClick(x, y, itemToSell, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QuestEssentialsMod.ModMonitor
                    .Log(
                        $"Error in {nameof(Before_receiveRightClick)} harmony patch: {ex}",
                        LogLevel.Error
                    );
            }
        }

        public static void After_hasTemporaryMessageAvailable(NPC __instance, ref bool __result)
        {
            bool ActiveQuestTalkFilter(TalkQuest q) => q.IsInQuestLog() 
                && !q.GetInQuestLog().ShouldDisplayAsComplete() 
                && q.TalkTo == __instance.Name;

            if (QuestEssentialsMod.QuestApi.GetAllManagedQuests<TalkQuest>().Any(ActiveQuestTalkFilter))
            {
                __result = true;
            }
        }

        private static int CalculatePrice(int sellPercentage, Item itemToSell)
        {
            int itemPrice = (int)(itemToSell is StardewValley.Object objToSell 
                ? (objToSell.sellToStorePrice(-1L) * sellPercentage) 
                : (float)(itemToSell.salePrice() / 2) * sellPercentage);

            return itemPrice * itemToSell.Stack;
        }

        public static void Before_set_Money(Farmer __instance, int value)
        {
            int oldMoney = __instance._money;
            __instance._money = value;

            if (value <= oldMoney)
                return;

            QuestCheckers.CheckEarnQuests(value - oldMoney);
        }

        public static bool Before_tryToReceiveActiveObject(NPC __instance, Farmer who)
        {
            if (__instance.Name.Equals("Henchman") && Game1.currentLocation.Name.Equals("WitchSwamp"))
                return true;

            if (QuestEssentialsMod.QuestApi.CheckForQuestComplete(new DeliverMessage(who, __instance, who.ActiveObject)))
            {
                if (who.ActiveObject.Stack <= 0)
                {
                    who.ActiveObject = null;
                    who.showNotCarrying();
                }

                return false;
            }

            return true;
        }
    }
}
