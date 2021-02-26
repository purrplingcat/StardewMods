using StardewValley;

namespace QuestEssentials.Messages
{
    public class EquipMessage : StoryMessage
    {
        public Farmer Farmer { get; }
        public Item UnequipedItem { get; }
        public Item EquipedItem { get; }
        public string EquipmentType { get; }

        public EquipMessage(Farmer farmer, Item unequipedItem, Item equipedItem, string equipmentType) : base("Equip")
        {
            this.Farmer = farmer;
            this.UnequipedItem = unequipedItem;
            this.EquipedItem = equipedItem;
            this.EquipmentType = equipmentType;
        }
    }
}
