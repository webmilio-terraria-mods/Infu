using System;

namespace Infu.Data.Armors;

[Flags]
public enum ArmorType
{
    Clothes = 1,
    Light = Clothes << 1,
    Heavy = Light << 1,
    Robes = Heavy << 1,
}
