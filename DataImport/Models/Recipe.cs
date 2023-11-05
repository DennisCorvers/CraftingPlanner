using DataImport.EqualityComparers;
using System.Diagnostics;

namespace DataImport.Models
{
    [DebuggerDisplay("{Output}")]
    public class Recipe
    {
        public IReadOnlySet<ItemStack> Input { get; }

        public ItemStack Output { get; }

        public Recipe(IEnumerable<ItemStack> input, ItemStack output)
        {
            Input = new HashSet<ItemStack>(input);
            Output = output;
        }

        public virtual bool HasOutput(Item other)
            => Output.HasItem(other);

        public virtual bool HasInput(Item other)
            => Input.Any(x => x.HasItem(other));
    }
}
