namespace CraftingPlannerLib.Entities
{
    public record class Recipe
    {
        public double OutputAmount { get; }

        public Item OutputItem { get; }

        public IDictionary<Item, double> InputItems { get; }

        public Recipe(Item parent, double outputAmount, IDictionary<Item, double> inputItems)
        {
            if (outputAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(outputAmount), "Amount needs to be at least one.");

            if (inputItems.Count == 0)
                throw new ArgumentException("Recipe needs at least one ingredient.", nameof(inputItems));

            OutputItem = parent;
            OutputAmount = outputAmount;
            InputItems = inputItems;
        }

        public bool HasItemAsIngredient(Item item)
            => InputItems.ContainsKey(item);

        public bool HasItemAsOutput(Item item)
            => OutputItem == item;

        public virtual bool Equals(Recipe? other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other is null)
                return false;

            return OutputAmount.Equals(other.OutputAmount) &&
                OutputItem.Equals(other.OutputItem) &&
                InputItems.Count == other.InputItems.Count &&
                InputItems.SequenceEqual(other.InputItems);
        }

        public override int GetHashCode()
            => base.GetHashCode();
    }
}