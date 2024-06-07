using CraftingPlannerLib.EqualityComparers;
using System.Diagnostics;

namespace CraftingPlannerLib.Models
{
    [DebuggerDisplay("{Amount} {Item}")]
    public record class ItemStack
    {
        public Item Item { get; }

        public int Amount { get; }

        public ItemStack(Item item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public virtual bool HasItem(Item item)
        {
            return BaseModelComparer.Default.Equals(Item, item);
        }
    }

    public record class AlternativeItemStack : ItemStack
    {
        public IReadOnlySet<Item> AlternativeItems { get; }

        public AlternativeItemStack(Item item, int amount, IEnumerable<Item> alternativeItems)
            : base(item, amount)
        {
            AlternativeItems = new HashSet<Item>(alternativeItems, BaseModelComparer.Default);
        }

        public override bool HasItem(Item item)
        {
            return base.HasItem(item) || AlternativeItems.Contains(item);
        }
    }
}
