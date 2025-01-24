using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infu;

public interface IPart
{
    public void AddPart(string key, IPart part);

    public IEnumerable<IPart>? GetParts(string key);

    public IEnumerable<T>? GetParts<T>(string key) where T : IPart;

    public IEnumerable<string> GetPartKeys();
}
