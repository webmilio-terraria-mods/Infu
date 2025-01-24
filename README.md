> This is a successor to [Infuller](https://github.com/webmilio-terraria-mods/Infuller) since I disliked both the name and the codebase.

Infu is a "information database library" that you can use in your projects to figure out what kind of item, npc, projectile, etc. things are.
The goal is to document everything that is not part of the vanilla game (i.e. what weapon is a fire-type weapon, what NPC is considered a zombie, etc.), 
but since this is a manual process, completion is not guaranteed. 

The design of this version revolves around "parts" (see [IPart](https://github.com/webmilio-terraria-mods/Infu/blob/master/IPart.cs) and it's abstract implementation
[Entry](https://github.com/webmilio-terraria-mods/Infu/blob/master/Entry.cs)) as well as the [PartRegister](https://github.com/webmilio-terraria-mods/Infu/blob/master/PartRegister.cs) and [DataRegister](https://github.com/webmilio-terraria-mods/Infu/blob/master/Data/DataRegister.cs).

Documentation is not planned for this mod since it's relatively straight-forward.
For example, getting the parts for an item is as simple as doing
```cs
public void SomeFunction(Item item) {
  var allParts = item.GetParts();
  var magicParts = item.GetParts<Magic>();
  var isArmor = item.HasPart<Armor>();
}
```

Although I'm quite sad (and uncertain) about this, I'm not convinced this mod is very efficient/optimized on it's calls. Therefor,
it's best if you build your own information cache after using this mod. For example, if you want all fire magic weapons to do +10% damage
with a certain accessory, you would cache this information on load.
```cs
private HashSet<int> _fireMagicWeapons = [];

public void OnLoad() {
  foreach 
  _fireMagicWeapons.Add()
}
```