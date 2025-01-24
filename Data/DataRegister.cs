using log4net;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace Infu.Data;

public class DataRegister
{
    private readonly ILog _logger;
    private readonly PartRegister _parts;

    private readonly Dictionary<int, Root> _items = [];

    public DataRegister(ILog logger, PartRegister parts)
    {
        _logger = logger;
        _parts = parts;
    }

    public IEnumerable<T> GetItemParts<T>(int type) where T : IPart
    {
        if (!_parts.Reverse<T>(out var key))
        {
            return Enumerable.Empty<T>();
        }

        if (!TryGetItem(type, out var root))
        {
            return Enumerable.Empty<T>();
        }

        return root.GetParts<T>(key);
    }

    public bool TryGetItem(int type, out Root root)
    {
        return _items.TryGetValue(type, out root!);
    }

    public void RegisterItem(Mod sender, int type, Root root)
    {
        if (_items.ContainsKey(type))
        {
            _logger.Error($"Mod {sender.Name} tried registering item type {type} which is already registered, canceling registration.");
            return;
        }

        _items.Add(type, root);
    }

    public int ItemCount => _items.Count;
}
