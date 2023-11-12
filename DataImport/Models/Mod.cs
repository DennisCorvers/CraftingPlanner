using DataImport.Utils;

namespace DataImport.Models
{
    public sealed class Mod : BaseModel
    {
        public IReadOnlyList<Item> Items { get; }

        public Mod(int id, string name, IEnumerable<Item> items)
            : base(id, NameFormatting.FormatName(name))
        {
            Items = items
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}
