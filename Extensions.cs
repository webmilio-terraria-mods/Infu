using Infu.Data;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Infu;

public static class Extensions
{
    public static IEnumerable<T> GetParts<T>(this Item item) where T : IPart
    {
        return ModContent.GetInstance<DataRegister>().GetItemParts<T>(item.type);
    }

    public static IEnumerable<T> FindParts<T>(this IPart root) where T : IPart
    {
        foreach (var key in root.GetPartKeys())
        {
            var parts = root.GetParts(key);

            foreach (IPart part in parts)
            {
                if (part is T i)
                {
                    yield return i;
                }

                var recurse = FindParts<T>(part);

                foreach (IPart local in recurse)
                {
                    if (local is T j)
                    {
                        yield return j;
                    }
                }
            }
        }
    }

    public static bool HasPart<T>(this IPart root)
    {
        foreach (var key in root.GetPartKeys())
        {
            var parts = root.GetParts(key);

            foreach (IPart part in parts)
            {
                if (part is T)
                {
                    return true;
                }

                return HasPart<T>(part);
            }
        }

        return false;
    }
}
