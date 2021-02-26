using Netcode;
using QuestEssentials.Messages;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Framework
{
    internal class EquipmentObserver
    {
        private readonly PerScreen<Farmer> _player = new PerScreen<Farmer>();

        public void HookUp(Farmer player)
        {
            player.shirtItem.fieldChangeEvent += this.OnClothChange;
            player.pantsItem.fieldChangeEvent += this.OnClothChange;
            player.boots.fieldChangeEvent += this.OnBootsChange;
            player.hat.fieldChangeEvent += this.OnHatChange;
            player.leftRing.fieldChangeEvent += this.OnRingChanged;
            player.rightRing.fieldChangeEvent += this.OnRingChanged;

            this._player.Value = player;
        }

        private void OnRingChanged(NetRef<Ring> field, Ring oldValue, Ring newValue)
        {
            this.DoEquipChange(oldValue, newValue, "Ring");
        }

        private void OnHatChange(NetRef<Hat> field, Hat oldValue, Hat newValue)
        {
            this.DoEquipChange(oldValue, newValue, "Hat");
        }

        private void OnBootsChange(NetRef<Boots> field, Boots oldValue, Boots newValue)
        {
            this.DoEquipChange(oldValue, newValue, "Boots");
        }

        private void OnClothChange(NetRef<Clothing> field, Clothing oldValue, Clothing newValue)
        {
            this.DoEquipChange(oldValue, newValue, "Cloth");
        }

        private void DoEquipChange(Item unequipedItem, Item equipedItem, string type)
        {
            if (unequipedItem == equipedItem)
                return;

            QuestEssentialsMod.QuestApi.CheckForQuestComplete(new EquipMessage(this._player.Value, unequipedItem, equipedItem, type));
        }

        internal bool PlayerEquips(string acceptedContextTags)
        {
            return false;
        }
    }
}
