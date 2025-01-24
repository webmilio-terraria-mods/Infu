using System.Collections.Generic;

namespace Infu;

public interface IPart
{
    public void AddPart(string key, IPart part);

    public IEnumerable<IPart> GetParts();
    public IEnumerable<string> GetPartKeys();

    public IEnumerable<IPart>? GetParts(string key);
}
