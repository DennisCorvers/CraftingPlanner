using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.Calculation
{
    public class CalculationResult
    {
        public Item TargetItem { get; }

        public int Amount { get; }

        public IReadOnlyDictionary<Item, int> TotalCosts { get; }

        public IReadOnlyDictionary<Item, int> Leftovers { get; }

        public CalculationResult(Item targetItem, int amount, IReadOnlyDictionary<Item, int> totalCosts, IReadOnlyDictionary<Item, int> leftovers)
        {
            TargetItem = targetItem;
            Amount = amount;
            TotalCosts = totalCosts;
            Leftovers = leftovers;
        }
    }
}
