using Infu.Data;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Infu.Extensions;

public static class Extensions
{
    public static IEnumerable<IPart> GetParts(this Item item)
    {
        return ModContent.GetInstance<DataRegister>().GetItemParts(item.type);
    }

    public static IEnumerable<T> GetParts<T>(this Item item) where T : IPart
    {
        return ModContent.GetInstance<DataRegister>().GetItem(item.type)?.GetParts<T>() ?? [];
    }
}
