using Newtonsoft.Json;
using QuestFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Quests.Story
{
    public class StoryQuestTask
    {
        private StoryQuest _quest;

        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> When { get; set; }
        public Dictionary<string, string> Data { get; set; }

        public int Goal { get; set; } = 1;

        [JsonIgnore]
        public int Current
        {
            get
            {
                if (this._quest == null)
                    return 0;

                if (!this._quest.State.ContainsKey(this.Name))
                {
                    this._quest.State[this.Name] = 0;
                    this._quest.Sync();
                }

                return this._quest.State[this.Name];
            }
            set
            {
                if (this._quest != null)
                {
                    this._quest.State[this.Name] = value;
                    this._quest.Sync();
                }
            }
        }

        protected bool IsWhenMatched()
        {
            if (this._quest == null)
                return false;

            if (this.When == null || this.When.Count == 0)
                return true;

            return this._quest.CheckGlobalConditions(this.When);
        }

        public bool IsRegistered()
        {
            return this._quest != null;
        }

        public void Increment(int amount)
        {
            this.Current += amount;
        }

        public bool IsCompleted()
        {
            return this.Current >= this.Goal;
        }

        public virtual void Load()
        {
        }

        public virtual void Register(StoryQuest quest)
        {
            this._quest = quest;
            this.Load();
        }

        public virtual void OnCompletionCheck(StoryMessage message)
        {
            if (this.Type == message.Trigger && !this.IsCompleted() && this.IsWhenMatched())
            {
                this.Increment(1);
            }
        }
    }
}
