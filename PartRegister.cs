using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Infu;

public class PartRegister
{
    private readonly Dictionary<string, Type> _byKey = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<Type, string> _byType = [];

    public bool Reverse<T>(out string key) where T : IPart
    {
        return _byType.TryGetValue(typeof(T), out key!);
    }

    public bool TryGetType(string key, out Type type)
    {
        return _byKey.TryGetValue(key, out type!);
    }

    public PartRegister Register<T>(Mod mod, string key) where T : IPart
    {
        Register<T>($"{mod.Name}.{key}");
        return this;
    }

    internal PartRegister Register<T>(string key) where T : IPart
    {
        _byKey.Add(key, typeof(T));
        _byType.Add(typeof(T), key);

        return this;
    }
}
