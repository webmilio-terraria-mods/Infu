using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Terraria.ModLoader;

namespace Infu.Data;

public class DataSerializer
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = {
            new JsonStringEnumConverter()
        }
    };

    public static IEnumerable<Entry> Process(Mod mod, IEnumerable<string> paths) => paths
        .Select(x => Deserialize(mod, x)).SelectMany(x => x);

    public static List<Entry> Deserialize(Mod mod, string path)
    {
        using var stream = mod.GetFileStream(path);
        var result = new List<Entry>();

        var entries = Deserialize(stream);
        result.AddRange(entries);

        return result;
    }

    public static List<Entry> Deserialize(Stream stream)
    {
        var entries = new List<Entry>();
        var documents = JsonSerializer.Deserialize<JsonDocument[]>(stream, _jsonOptions)!;

        foreach (var document in documents)
        {
            var entry = Deserialize(document);

            if (entry != null)
            {
                entries.Add(entry);
            }
        }

        return entries;
    }

    public static Entry? Deserialize(JsonDocument document)
    {
        var parts = ModContent.GetInstance<PartRegister>();
        return Deserialize(parts, document.RootElement, typeof(Root));
    }

    public static Entry? Deserialize(PartRegister parts, JsonElement element, Type type)
    {
        var entry = element.Deserialize(type, _jsonOptions) as Entry;
        var properties = element.EnumerateObject();

        if (entry == null)
        {
            return null;
        }

        foreach (var jsonProp in properties)
        {
            if (!parts.TryGetType(jsonProp.Name, out var partType))
            {
                continue;
            }

            var part = Deserialize(parts, jsonProp.Value, partType);
            entry.AddPart(jsonProp.Name, part);
        }

        return entry;
    }
}
