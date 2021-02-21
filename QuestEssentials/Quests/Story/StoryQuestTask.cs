using Newtonsoft.Json;
using QuestEssentials.Framework;
using QuestEssentials.Quests.Story.Tasks;
using QuestFramework.Extensions;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Quests.Story
{
    [JsonConverter(typeof(StoryQuestTaskConverter))]
    public abstract class StoryQuestTask
    {
        internal static readonly Dictionary<string, Type> knownTypes;
        private StoryQuest _quest;
        protected bool _complete;

        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> When { get; set; }
        public int Goal { get; set; } = 1;

        [JsonIgnore]
        public int Current
        {
            get
            {
                if (this._quest == null || this._quest.State == null)
                    return 0;

                if (!this._quest.State.progress.ContainsKey(this.Name))
                {
                    this._quest.State.progress[this.Name] = 0;
                    this._quest.Sync();
                }

                return this._quest.State.progress[this.Name];
            }
            set
            {
                if (this._quest != null || this._quest.State == null)
                {
                    this._quest.State.progress[this.Name] = value;
                    this._quest.Sync();
                    this.OnCurrentCountChanged();
                }
            }
        }

        static StoryQuestTask()
        {
            knownTypes = new Dictionary<string, Type>();

            RegisterTaskType<BasicTask>("Basic");
            RegisterTaskType<EnterSpotTask>("EnterSpot");
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
            return this._quest != null && this._quest.State != null;
        }

        public void Increment(int amount)
        {
            this.Current += amount;
        }

        public bool IsCompleted()
        {
            return this._complete;
        }

        public virtual void Load()
        {
        }

        public virtual void Register(StoryQuest quest)
        {
            if (quest.State == null)
            {
                quest.Reset();
            }

            this._quest = quest;
            this.Load();
        }

        protected virtual void OnCurrentCountChanged()
        {
            this.CheckCompletion();
        }

        protected virtual void OnCompletion()
        {
        }

        public abstract bool OnCheckProgress(StoryMessage message);

        public virtual bool CheckCompletion(bool playSound = true)
        {
            if (!this.IsRegistered())
                return false;

            bool wasJustCompleted = false;

            if (this.Current >= this.Goal && !this.IsCompleted())
            {
                wasJustCompleted = true;
                this._complete = true;
                this.OnCompletion();
            }

            if (this._quest != null)
            {
                this._quest.CheckQuestCompletion();
                if (playSound && wasJustCompleted && !this._quest.State.complete)
                {
                    Game1.playSound("jingle1");
                }
            }

            return wasJustCompleted;
        }

        public void ForceComplete(bool playSound = true)
        {
            this.Current = this.Goal;
        }

        /// <summary>
        /// Register a class for deserialize for specified type name
        /// Unknown type names are deserialized as <see cref="StoryQuestTask"/> class type.
        /// </summary>
        /// <typeparam name="T">Task class type</typeparam>
        /// <param name="name">Name of task type</param>
        public static void RegisterTaskType<T>(string name) where T : StoryQuestTask
        {
            knownTypes.Add(name, typeof(T));
        }

        public static bool IsKnownTaskType(string name)
        {
            return knownTypes.ContainsKey(name);
        }

        public static bool IsKnownTaskType(Type type)
        {
            return knownTypes.ContainsValue(type);
        }

        public static bool IsKnownTaskType<T>() where T : StoryQuestTask
        {
            return knownTypes.ContainsValue(typeof(T));
        }
    }
}
