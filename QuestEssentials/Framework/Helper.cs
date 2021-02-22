using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestEssentials.Framework
{
    internal static class Helper
    {
        public static bool CheckItemContextTags(Item item, string tags)
        {
            bool fail = false;

            foreach (string tagArray in tags.Split(','))
            {
                bool foundMatch = false;

                foreach (string tag in tagArray.Split('/'))
                {
                    if (item.HasContextTag(tag.Trim()))
                    {
                        foundMatch = true;
                        break;
                    }
                }

                if (!foundMatch)
                {
                    fail = true;
                }
            }

            return !fail;
        }
    }
}
