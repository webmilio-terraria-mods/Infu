using System.Collections.Generic;
using System.Linq;

namespace Infu.Extensions;

public static class PartExtensions
{
    /// <summary>Gets the parts of a specified type in the current part. Can be used to search only top-level parts or to recursively search inside of children.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="part"></param>
    /// <param name="topLevel"></param>
    /// <returns></returns>
    public static IEnumerable<T> GetParts<T>(this IPart part, bool topLevel = true) where T : IPart
    {
        var locals = part.GetParts();

        foreach (var local in locals)
        {
            if (local is T t)
            {
                yield return t;
            }

            if (!topLevel)
            {
                var remotes = GetParts<T>(local, false);

                foreach (var remote in remotes)
                {
                    yield return remote;
                }
            }
        }
    }

    /// <summary>Determines if provided part contains a part of the specified type. Can be used to search only top-level parts or to recursively search inside of children.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="part"></param>
    /// <param name="topLevel"></param>
    /// <returns></returns>
    public static bool HasPart<T>(this IPart part, bool topLevel = false)
    {
        var locals = part.GetParts();

        foreach (var local in locals)
        {
            if (local is T)
            {
                return true;
            }

            if (!topLevel && HasPart<T>(local, false))
            {
                return true;
            }
        }

        return false;
    }
}
