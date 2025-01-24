namespace Infu.Data.Armors;

public record Armor : Entry
{
    public ArmorType Type { get; init; }
}
