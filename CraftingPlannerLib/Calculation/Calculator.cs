using CraftingPlannerLib.EqualityComparers;
using CraftingPlannerLib.Models;
using CraftingPlannerLib.Services;
using System.Runtime.CompilerServices;

namespace CraftingPlannerLib.Calculation
{
    internal class Calculator
    {
        private readonly Dictionary<Item, int> m_totalCost;
        private readonly Dictionary<Item, int> m_leftovers;
        private readonly CalculationService m_calculationService;
        private readonly IEqualityComparer<Item> m_equalityComparer;

        private readonly Item m_targetItem;

        public Calculator(CalculationService recipeService, Item target)
        {
            m_totalCost = new Dictionary<Item, int>(BaseModelComparer.Default);
            m_leftovers = new Dictionary<Item, int>(BaseModelComparer.Default);
            m_calculationService = recipeService;
            m_equalityComparer = BaseModelComparer.Default;

            m_targetItem = target;
        }

        public CalculationResult Calculate(int amount)
        {
            Calculate(m_targetItem, amount);
            return new CalculationResult(m_targetItem, amount, m_totalCost, m_leftovers);
        }

        private void Calculate(Item currentItem, int amount)
        {
            var recipe = m_calculationService.GetItemRecipe(m_targetItem);
            if (recipe == null)
                return;

            // Calculate how many times this recipe should be repeated.
            var craftingIterations = GetCraftingIterations(currentItem, amount, recipe);

            // Add output items to leftovers list.
            foreach (ItemStack output in recipe.Output)
            {
                var oItem = output.Item;
                var oAmount = output.Amount * craftingIterations;

                // Subtract current requested item from the amount.
                // We are only interested in the lastover items here.
                if (oItem == currentItem)
                    oAmount -= amount;

                AddOrSet(m_leftovers, oItem, oAmount);
            }

            foreach (ItemStack input in recipe.Input)
            {
                var iItem = input.Item;
                var iAmount = input.Amount * craftingIterations;

                // Try to grab required items from spare item list first.
                var amountLeft = GetSpareItems(iItem, iAmount);

                // If we already have the item/amount satisfied, skip this item.
                if (amountLeft <= 0)
                {
                    continue;
                }

                AddOrSet(m_totalCost, iItem, amountLeft);
                Calculate(iItem, amountLeft);
            }
        }

        private int GetCraftingIterations(Item targetItem, int targetAmount, Recipe recipe)
        {
            foreach (ItemStack output in recipe.Output)
            {
                if (m_equalityComparer.Equals(targetItem, output.Item))
                {
                    return (int)Math.Ceiling((double)targetAmount / output.Amount);
                }
            }
            return 0;
        }

        private int GetSpareItems(Item item, int amount)
        {
            if (m_leftovers.TryGetValue(item, out var spareCount))
            {
                spareCount -= amount;
                if (spareCount > 0)
                {
                    m_leftovers[item] = spareCount;
                    return 0;
                }
                else
                {
                    m_leftovers.Remove(item);
                    return -spareCount;
                }
            }
            else
            {
                return amount;
            }
        }

        private static void AddOrSet(IDictionary<Item, int> dictionary, Item item, int amount)
        {
            if (dictionary.ContainsKey(item))
            {
                dictionary[item] += amount;
            }
            else
            {
                dictionary.Add(item, amount);
            }
        }
    }
}
