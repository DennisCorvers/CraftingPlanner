using CraftingPlannerLib.EqualityComparers;
using CraftingPlannerLib.Models;
using CraftingPlannerLib.Services;

namespace CraftingPlannerLib.Calculation
{
    internal class Calculator
    {
        private readonly Dictionary<Item, int> m_totalCost;
        private readonly Dictionary<Item, int> m_leftovers;

        private readonly Item m_targetItem;

        public Calculator(CalculationService recipeService, Item target)
        {
            m_totalCost = new Dictionary<Item, int>(BaseModelComparer.Default);
            m_leftovers = new Dictionary<Item, int>(BaseModelComparer.Default);

            m_targetItem = target;
        }

        public void Calculate(int amount)
        {
            m_totalCost.Clear();
            m_leftovers.Clear();

        }
    }
}
