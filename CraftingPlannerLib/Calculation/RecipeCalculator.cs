using System.Runtime.CompilerServices;
using CraftingPlannerLib.Entities;

namespace CraftingPlannerLib.Calculation
{
    public class RecipeCalculator : IRecipeCalculator
    {
        //public int MaxRecursionCount { get; }

        //public RecipeCalculator(byte maxRecursionCount = 32)
        //{
        //    MaxRecursionCount = maxRecursionCount;
        //}

        ///// <summary>
        ///// Collects all the required items and their amounts to craft the requested item.
        ///// </summary>
        ///// <param name="baseItem">The item to find the required items for.</param>
        ///// <param name="requiredAmount">The start amount</param>
        ///// <param name="returnbase">TRUE if only the last items in the chain should be collected.</param>
        //public Dictionary<Item, double> CalculateRecipe(Item baseItem, double requiredAmount = 1, bool returnbase = false)
        //{
        //    var recipe = new Dictionary<Item, double>();

        //    CalculateItemRecipe(baseItem, requiredAmount, returnbase, recipe, 0);

        //    return recipe;
        //}

        //private void CalculateItemRecipe(Item item, double amount, bool isBaseItem, Dictionary<Item, double> craftingRecipe, int recursionCount)
        //{
        //    if (++recursionCount >= MaxRecursionCount)
        //        return;

        //    foreach (var pair in item.Recipe)
        //    {
        //        if (isBaseItem)
        //        {
        //            if (pair.Key.Recipe.Count <= 0)
        //            {
        //                AddToRecipe(pair);
        //                continue;
        //            }
        //        }
        //        else
        //        {
        //            AddToRecipe(pair);

        //            if (pair.Key.Recipe.Count <= 0)
        //                continue;
        //        }

        //        double tempAmount = amount * pair.Value;
        //        CalculateItemRecipe(pair.Key, tempAmount, isBaseItem, craftingRecipe, recursionCount);
        //    }

        //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //    void AddToRecipe(KeyValuePair<Item, double> recipeItem)
        //    {
        //        if (craftingRecipe.ContainsKey(recipeItem.Key))
        //            craftingRecipe[recipeItem.Key] += recipeItem.Value * amount;
        //        else
        //            craftingRecipe.Add(recipeItem.Key, recipeItem.Value * amount);
        //    }
        //}
        Dictionary<Item, double> IRecipeCalculator.CalculateRecipe(Item baseItem, double requiredAmount, bool returnbase)
        {
            throw new NotImplementedException();
        }
    }
}
