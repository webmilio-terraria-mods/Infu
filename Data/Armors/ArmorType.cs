using System;

namespace Infu.Data.Armors;

/// <summary>
///     A <b>Flag</b> enum to classify types of armor. To correctly compare values, you should
///     never use <c>==</c> or <c>Equals</c> but <see cref="Enum.HasFlag(ArmorType)" /> where <see cref="Enum" /> is your field.
/// <br />
/// <br />
/// <example>
///     var armorPart = ... // we assume you have an <see cref="Armor"/> part.<br />
///     var isHeavy = armorPart.HasFlag(<see cref="Heavy"/>);
/// </example>
/// </summary>
[Flags]
public enum ArmorType
{
    None = 0,
    Cloth = 1,
    Light = Cloth << 1,
    Heavy = Light << 1,

    Universal = Cloth | Light | Heavy
}
