> This is a successor to [Infuller](https://github.com/webmilio-terraria-mods/Infuller) since I disliked both the name and the codebase.

Infu is a "information database library" that you can use in your projects to figure out what kind of item, npc, projectile, etc. things are.
The goal is to document everything that is not part of the vanilla game (i.e. what weapon is a fire-type weapon, what NPC is considered a zombie, etc.), 
but since this is a manual process, completion is not guaranteed. Information is also subjective, i.e. Wooden armor is light armor but Ebonwood armor is heavy armor.
I welcome all information contributions or corrections via the form of Pull Requests.

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

Although I'm quite sad (and uncertain) about this, I'm not convinced this mod is very efficient/optimized on it's calls. Therefore,
it's best if you build your own information cache after using this mod. For example, if you want armor to reduce movement speed by 10%
with heavy armor, you would cache this information on load.
```cs
    public HashSet<int> heavyArmor = [];
    public void OnLoad()
    {
        var register = ModContent.GetInstance<DataRegister>();
    
        for (int i = 0; i < ItemLoader.ItemCount; i++)
        {
            if (!register.TryGetItem(i, out var root))
            {
                // Skip this item since it doesn't have any parts registered.
                continue;
            }
    
            // topLevel is false since we want to search all parts and their children.
            if (root.TryGetParts<Armor>(out var armors, topLevel: false) && 
                armors.Any(a => a.Type.HasFlag(ArmorType.Heavy)))
            {
                heavyArmor.Add(i);
            }
        }
    }

    // ...
    // We want to know if we should slow the player, presumably because he's wearing armor.
    public bool ShouldSlowPlayer(int itemType)
    {
        return heavyArmor.Contains(itemType);
    }
}
```