using System;

namespace Infu.Data.Armors;

[Flags]
public enum ArmorType
{
    Cloth = 1,
    Light = Cloth << 1,
    Heavy = Light << 1,
}
