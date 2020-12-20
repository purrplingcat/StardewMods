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

## Configuration

You can configure this mod via `config.json` file in the mod folder:

```js
{
  "CustomBirthdayDialogues": true, // Allow show birthday gift custom reaction dialogues (if they are defined)
  "CustomSecretSantaDialogues": true // Allow show secret santa gift custom reaction dialogues (if they are defined)
}
```

## Create gift reaction dialogues

Add custom gift dialogues is very easy. Just add into game content `Characters/Dialogue/<NpcName>` and add lines witch these possible keys:

- `GiftReaction_<ObjectName>` - Reaction dialogue for concrete gifted item. The `<ObjectName>` is a name of an object for which your NPC speaks a dialogue when this item was gifted to them. Spaces in the object name must be replaced with underscore `_` in the dialogue line key.
- `GiftReactionCategory_<CategoryNumber>` - Reaction for all object of concrete category.
- `GiftReaction_<ObjectName>_Birthday` - Custom reaction to an item if this item was gifted on NPC's birthday. Object name has the same rules like for general custom gift object reaction
- `GiftReactionCategory_<CategoryNumber>_Birthday` - Same as for birthday object reaction, but for all objects in specified category
- `GiftReaction_<ObjectName>_SecretSanta` - Custom reaction to an item if this item was gifted as secret santa during winterstar festival. Object name has the same rules like for general custom gift object reaction
- `GiftReactionCategory_<CategoryNumber>_SecretSanta` - Same as for secret santa object reaction, but for all objects in specified category


The `<NpcName>` (in the content asset name) is a name of NPC for which you want add custom gift reaction dialogue lines.
Also you can add some alternate lines for gifted item by adding suffix `~<number>` after the dialogue key. (Works only for gift reaction dialogues)

**NOTE:** Mod seeks for concrete gift object dialogue first, then for an object category. That means if you specify reaction for seeds category (id number -74, dialogue key `GiftReactionCategory_-74`) and for object *Parsnip Seeds* (`GiftReaction_Parsnip_Seeds`), then if you gift Parsnip Seeds to your concrete NPC, then you see dialogue for *Pasrsnip Seeds*. If you gift other seeds, you see dialogue for seeds category.

Most simple way how to add custom gift reaction dialogues is do it with [Content Patcher](https://github.com/Pathoschild/StardewMods/blob/stable/ContentPatcher/docs/author-guide.md):

```js
{
  "Format": "1.19.0",
  "Changes": [
    {
      // Custom gift reaction for Chocolate Cake with some randomized alternatives gifted to Abigail
      "Action": "EditData",
      "Target": "Characters/Dialogue/Abigail",
      "Entries": {
        "GiftReaction_Chocolate_Cake": "I love chocolate cake, thank you so much, @!$h",
        "GiftReaction_Chocolate_Cake~1": "How are you know I'm hungry? This cake looks delicious, Thank you!$h",
        "GiftReaction_Chocolate_Cake~2": "Looks really tasty, thanks$l",
        "GiftReaction_Chocolate_Cake~3": "It's shame I wanted to eat a rock for today's lunch.#$b#Just kidding, it looks really delicious! Thank you, @.$h"
      }
    },
    {
      // Custom gift reaction for an object category with exception for object Parsnip Seeds gifted to Lewis
      "Action": "EditData",
      "Target": "Characters/Dialogue/Lewis",
      "Entries": {
        "GiftReaction_Parsnip_Seeds": "A dialogue for parsnip seeds",
        "GiftReactionCategory_-74": "A dialogue for general seeds" // -74 is seeds category id
      }
    }
  ]
}
```

---

This mod is made with :heart: by PurrplingCat. Thanks to Lemurkat for an idea on [StardewModders mod ideas](https://github.com/StardewModders/mod-ideas/issues/611).


