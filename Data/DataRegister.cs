using Infu.Extensions;
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

    public IEnumerable<IPart> GetItemParts(int type)
    {
        if (!TryGetItem(type, out var root))
        {
            return [];
        }

        return root.GetParts();
    }

    public Root? GetItem(int type)
    {
        return TryGetItem(type, out var root) ? root : null;
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
