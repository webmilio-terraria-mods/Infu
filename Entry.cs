using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Infu;

public record Entry : IPart
{
    private record DictEntry(List<IPart> Source, ReadOnlyCollection<IPart> Front);

    private readonly Dictionary<string, DictEntry> _partKeys = new(StringComparer.OrdinalIgnoreCase);

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

    public IEnumerable<IPart> GetParts(string key)
    {
        if (_partKeys.TryGetValue(key, out var parts))
        {
            return parts.Front;
        }

        return Enumerable.Empty<IPart>();
    }

    public IEnumerable<T> GetParts<T>(string key) where T : IPart
    {
        var parts = GetParts(key);

        foreach (var part in parts)
        {
            if (part is T t)
            {
                yield return t;
            }
        }
    }

    public IEnumerable<string> GetPartKeys()
    {
        return _partKeys.Keys;
    }
}

public record Root : Entry
{
    public int Id { get; init; }
}
