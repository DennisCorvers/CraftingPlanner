using DataImport.Utils;

namespace DataImport.Models
{
    public sealed class Mod : BaseModel
    {
        public IReadOnlySet<Item> Items { get; }

        public Mod(int id, string name, HashSet<Item> items)
            : base(id, NameFormatting.FormatName(name))
        {
            Items = items;
        }
    }
}
