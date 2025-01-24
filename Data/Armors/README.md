To correctly assert [ArmorTypes](ArmorType.cs), you should never use `==` or `Equals` but `HasFlag`. This
is because [ArmorType](ArmorType.cs) is a Flag enum and can have multiple values at the same time.

```cs
var armorPart = ... // we assume you have an Armor part.
var isHeavy = armorPart.HasFlag(ArmorType.Heavy);
```