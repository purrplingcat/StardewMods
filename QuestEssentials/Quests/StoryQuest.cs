using Newtonsoft.Json;
using QuestFramework.Quests;
using QuestFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestEssentials.Quests.Story;

namespace QuestEssentials.Quests
{
    public class StoryQuest : CustomQuest<Dictionary<string, int>>
    {
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
            if (!this.Tasks.Any(t => t.IsRegistered() && !t.IsCompleted()))
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

        private void CheckTasks(StoryMessage storyMessage)
        {
            foreach (var task in this.Tasks)
            {
                if (!task.IsRegistered())
                    continue;

                task.OnCompletionCheck(storyMessage);
            }
        }
    }
}
