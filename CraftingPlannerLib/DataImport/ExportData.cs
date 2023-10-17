namespace CraftingPlannerLib.DataImport
{
    [Serializable]
    internal class ExportData
    {
        public ExportData(int lastUID, List<ItemEntity> items, List<ModEntity> mods, List<ItemTypeEntity> itemTypes)
        {
            LastUID = lastUID;
            Items = items;
            Mods = mods;
            ItemTypes = itemTypes;
        }

        public int LastUID { get; }

        public List<ItemEntity> Items { get; }

        public List<ModEntity> Mods { get; }

        public List<ItemTypeEntity> ItemTypes { get; }
    }
}
