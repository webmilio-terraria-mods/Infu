using System;

namespace Infu.Data.Armors;

[Flags]
public enum ArmorType
{
    None = 0,
    Cloth = 1,
    Light = Cloth << 1,
    Heavy = Light << 1,

    Universal = Cloth | Light | Heavy
}
