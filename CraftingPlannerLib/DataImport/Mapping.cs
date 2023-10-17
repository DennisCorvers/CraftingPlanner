using CraftingPlannerLib.Entities;

namespace CraftingPlannerLib.DataImport
{
    internal class Mapping
    {
        internal Mod Map(ModEntity other)
            => new(other.Name) { Id = other.ID };

        internal ItemType Map(ItemTypeEntity other)
            => new(other.Name) { Id = other.ID };

        internal Item Map(ItemEntity other)
            => new(other.Name) { Id = other.ID };

        internal Item Map(ItemEntity other, Mod? mod, ItemType? itemType, Dictionary<Item, double>? recipe)
            => new(other.Name, itemType, mod, recipe) { Id = other.ID };
    }
}
