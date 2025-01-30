using Infu.Data;
using Infu.Parts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.ModLoader;

namespace Infu;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public class InfuMod : Mod
{
    public override void Load()
    {
        RegisterParts();
        LoadData();
    }

    private void RegisterParts()
    {
        ContentInstance.Register(new PartRegister());

        var parts = ModContent.GetInstance<PartRegister>();

        parts
            .Register<Armor>(nameof(Armor));
    }

    private void LoadData()
    {
        var parts = ModContent.GetInstance<PartRegister>();
        ContentInstance.Register(new DataRegister(Logger, parts));

        var names = GetFileNames()
            .Where(static x =>
                Path.GetExtension(x).Equals(".json", StringComparison.OrdinalIgnoreCase) &&
                x.StartsWith("Data/", StringComparison.OrdinalIgnoreCase));

        var data = ModContent.GetInstance<DataRegister>();
        var entries = DataSerializer.Process(this, names);

        foreach (var entry in entries)
        {
            if (entry is not Root r)
            {
                continue;
            }

            data.RegisterItem(this, r.Id, r);
        }

        Logger.Info($"Loaded data for {data.ItemCount} items");
    }
}
