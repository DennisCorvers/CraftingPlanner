using System.Diagnostics;

namespace CraftingPlannerLib.Models
{
    [DebuggerDisplay("{FirstOutput}")]
    public class Recipe
    {
        public int Id { get; }

        public IReadOnlySet<ItemStack> Input { get; }

        public IReadOnlySet<ItemStack> Output { get; }

        // Used for debugger display
        private ItemStack? FirstOutput
            => Output.FirstOrDefault();

        public Recipe(int id, IEnumerable<ItemStack> input, IEnumerable<ItemStack> output)
        {
            Id = id;
            Input = new HashSet<ItemStack>(input);
            Output = new HashSet<ItemStack>(output);
        }

        public virtual bool HasOutput(Item other)
            => Output.Any(x => x.HasItem(other));

        public virtual bool HasInput(Item other)
            => Input.Any(x => x.HasItem(other));
    }
}
