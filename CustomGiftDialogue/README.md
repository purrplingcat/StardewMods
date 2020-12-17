# Custom Gift Dialogue

This mod allows define custom gift reaction dialogues for concrete gift items. Also this mod may be required by some other mods which adds custom gift reactions

**NOTE:** This mod doesn't add any new dialogue into game, it's only utillity for other modders and for players which want to play a mods add custom gift reaction dialogues requiring this mod.

## Requirements

- [StardewModding API (SMAPI)](https://smapi.io)
- A legal copy of [Stardew Valley](https://stardewvalley.net)

## Compatibility

- Compatible with most SMAPI mods and Content Patcher
- Compatible with JSON Assets

## Installation

This installation steps predicates you have already installed StardewValley.

- Install SMAPI
- Download [this mod from Nexusmods](https://www.nexusmods.com/stardewvalley/mods/7304) and unpack it to `<GameFolder>/Mods`
- Install some mods which adds gift reaction dialogues (or create your own with [Content Patcher](https://www.nexusmods.com/stardewvalley/mods/1915)

## Create gift reaction dialogues

Add custom gift dialogues is very easy. Just add into game content `Characters/Dialogue/<NpcName>` and add lines witch key `GiftReaction_<ObjectName>`, where:

- The `<NpcName>` is a name of NPC for which you want add custom gift reaction dialogue lines.
- The `<ObjectName>` is a name of an object for which your NPC speaks a dialogue when this item was gifted to them. Spaces in the object name must be replaced with underscore `_` in the dialogue line key.

Also you can add some alternate lines for gifted item by adding suffix `~<number>` after the dialogue key. (Works only for gift reaction dialogues)

Most simple way how to add custom gift reaction dialogues is do it with [Content Patcher](https://github.com/Pathoschild/StardewMods/blob/stable/ContentPatcher/docs/author-guide.md):

```js
{
  "Format": "1.19.0",
  "Changes": [
    {
      "Action": "EditData",
      "Target": "Characters/Dialogue/Abigail",
      "Entries": {
        "GiftReaction_Chocolate_Cake": "I love chocolate cake, thank you so much, @!$h",
        "GiftReaction_Chocolate_Cake~1": "How are you know I'm hungry? This cake looks delicious, Thank you!$h",
        "GiftReaction_Chocolate_Cake~2": "Looks really tasty, thanks$l",
        "GiftReaction_Chocolate_Cake~3": "It's shame I wanted to eat a rock for today's lunch.#$b#Just kidding, it looks really delicious! Thank you, @.$h"
      }
    }
  ]
}
```

---

This mod is made with :heart: by PurrplingCat. Thanks to Lemurkat for an idea on [StardewModders mod ideas](https://github.com/StardewModders/mod-ideas/issues/611).


