using CraftingPlannerLib.DataImport.External;

namespace CraftingPlannerLib.DataImport.Models
{
    [Serializable]
    internal class ExportData
    {
        public ExportData(int lastUID, List<Item> items, List<Mod> mods, List<ItemType> itemTypes)
        {
            LastUID = lastUID;
            Items = items;
            Mods = mods;
            ItemTypes = itemTypes;
        }

        public int LastUID { get; }

        public List<Item> Items { get; }

        public List<Mod> Mods { get; }

        public List<ItemType> ItemTypes { get; }
    }
}
