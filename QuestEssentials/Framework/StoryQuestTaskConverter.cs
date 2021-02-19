using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuestEssentials.Quests.Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Framework
{
    internal class StoryQuestTaskConverter : JsonConverter
    {
        private bool _inside = false;

        public override bool CanWrite => false;
        public override bool CanRead => !this._inside;

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(StoryQuestTask));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                throw new JsonException();

            this._inside = true;
            JObject jObject = JObject.Load(reader);

            if (!jObject.ContainsKey("Type"))
            {
                throw new JsonException("Attribute `Type` is required for `StoryQuestTalk`!");
            }

            StoryQuestTask task;
            string type = jObject["Type"].ToString();
            if (!StoryQuestTask.knownTypes.ContainsKey(type))
            {
                throw new JsonException($"Unknown story quest type `{type}`");
            }

            QuestEssentialsMod.ModMonitor.Log($"StoryQuestTaskConverter: Using class type <{StoryQuestTask.knownTypes[type].FullName}> for `{type}`");
            task = (StoryQuestTask)jObject.ToObject(StoryQuestTask.knownTypes[type], serializer);
            this._inside = false;

            return task;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
