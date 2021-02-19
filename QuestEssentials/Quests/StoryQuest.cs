using Newtonsoft.Json;
using QuestFramework.Quests;
using QuestFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using QuestEssentials.Quests.Story;
using StardewModdingAPI;
using System.Text;

namespace QuestEssentials.Quests
{
    public class StoryQuest : CustomQuest<StoryQuest.StoryQuestState>
    {
        public class StoryQuestState
        {
            public bool complete = false;
            public Dictionary<string, int> progress;

            public StoryQuestState()
            {
                this.progress = new Dictionary<string, int>();
            }
        }

        public List<StoryQuestTask> Tasks { get; set; }

        protected override void OnInitialize()
        {
            this.Accepted += this.OnAccepted;
            QuestEssentialsMod.ModHelper.Events.GameLoop.UpdateTicked += this.OnGameUpdateTicked;
            base.OnInitialize();
        }

        private void OnAccepted(object sender, IQuestInfo e)
        {
            this.Tasks.ForEach(t => t.Register(this));
        }

        protected override void Dispose(bool disposing)
        {
            this.Accepted -= this.OnAccepted;
            QuestEssentialsMod.ModHelper.Events.GameLoop.UpdateTicked -= this.OnGameUpdateTicked;
            base.Dispose(disposing);
        }

        private void OnGameUpdateTicked(object sender, StardewModdingAPI.Events.UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady || !Context.CanPlayerMove)
                return;

            if (this.State.complete && this.IsInQuestLog() && !this.GetInQuestLog().completed.Value)
            {
                this.Complete();
            }
        }

        public override void OnRegister()
        {
            this.Tasks.ForEach(t => t.Register(this));
            base.OnRegister();
        }

        public override void OnCompletionCheck(object completionMessage)
        {
            if (completionMessage is StoryMessage storyMessage)
            {
                this.CheckTasks(storyMessage);
            }

            base.OnCompletionCheck(completionMessage);
        }

        protected override void UpdateCurrentObjectives(List<CustomQuestObjective> currentObjectives)
        {
            currentObjectives.Clear();

            if (this.Tasks != null) {
                foreach (var task in this.Tasks)
                {
                    StringBuilder text = new StringBuilder(task.Description);

                    if (task.Goal > 1)
                    {
                        text.Append($" ({task.Current}/{task.Goal})");
                    }

                    currentObjectives.Add(new CustomQuestObjective(task.Name, text.ToString())
                    {
                        IsCompleted = task.IsCompleted()
                    });
                }
            }
        }

        public void CheckQuestCompletion()
        {
            this.State.complete = !this.Tasks.Any(t => t.IsRegistered() && !t.IsCompleted());
        }

        private void CheckTasks(StoryMessage storyMessage)
        {
            if (this.Tasks == null)
                return;

            foreach (var task in this.Tasks)
            {
                if (!task.IsRegistered())
                    continue;

                task.OnCompletionCheck(storyMessage);
            }
        }
    }
}
