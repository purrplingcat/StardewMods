using QuestEssentials.Framework;
using QuestEssentials.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Tasks
{
    public class EquipTask : QuestTask<EquipTask.EquipData>
    {
        public struct EquipData
        {
            public string AcceptedContextTags { get; set; }
        }

        public override void Load()
        {
            if (QuestEssentialsMod.Instance.PlayerEquips(this.Data.AcceptedContextTags) && !this.IsCompleted())
            {
                this.IncrementCount(this.Goal);
            }

            base.Load();
        }

        public override bool ShouldShowProgress()
        {
            return false;
        }

        public override bool OnCheckProgress(StoryMessage message)
        {
            if (message is EquipMessage equipMessage)
            {
                int amount = 0;
                bool equiped = equipMessage.EquipedItem != null && Helper.CheckItemContextTags(equipMessage.EquipedItem, this.Data.AcceptedContextTags);
                bool unequiped = equipMessage.UnequipedItem != null && Helper.CheckItemContextTags(equipMessage.UnequipedItem, this.Data.AcceptedContextTags);

                if (unequiped)
                    amount = -this.Goal;
                if (equiped)
                    amount = this.Goal;

                if (amount != 0)
                    this.IncrementCount(amount);
            }

            return false;
        }
    }
}
