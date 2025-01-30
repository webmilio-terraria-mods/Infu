using Infu.Data;
using Infu.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Terraria.ModLoader;

namespace Infu.Parts;

public record Part : IPart
{
    private record DictPart(List<IPart> Source, ReadOnlyCollection<IPart> Front);

    private readonly Dictionary<string, DictPart> _partKeys = new(StringComparer.OrdinalIgnoreCase);

    public void AddPart(string key, IPart part)
    {
        if (!_partKeys.TryGetValue(key, out var wrap))
        {
            var parts = new List<IPart>();
            wrap = new(parts, parts.AsReadOnly());

            _partKeys.Add(key, wrap);
        }

        wrap.Source.Add(part);
    }

    public IEnumerable<IPart> GetParts()
    {
        return _partKeys.Select(x => x.Value.Front).SelectMany(x => x);
    }

    public IEnumerable<string> GetPartKeys()
    {
        return _partKeys.Keys;
    }

    public IEnumerable<IPart> GetParts(string key)
    {
        if (_partKeys.TryGetValue(key, out var parts))
        {
            return parts.Front;
        }

        return [];
    }

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
    // We want to know if we should the player, presumably because he's wearing armor.
    public bool ShouldSlowPlayer(int itemType)
    {
        return heavyArmor.Contains(itemType);
    }
}

public record Root : Part
{
    public int Id { get; init; }
}
