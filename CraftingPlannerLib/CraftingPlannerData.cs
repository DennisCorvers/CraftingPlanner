using CraftingPlannerLib.Tables;

namespace CraftingPlannerLib
{
    public class CraftingPlannerData
    {
        private ItemRepository m_items;

        private ModsTable m_mods;

        private ItemTypesTable m_itemTypes;

        public ModsTable Mods
            => m_mods;
        public ItemTypesTable ItemTypes
            => m_itemTypes;
        public ItemRepository Items
            => m_items;

        public CraftingPlannerData()
        {
            m_items = new ItemRepository(new());
            m_mods = new ModsTable(new());
            m_itemTypes = new ItemTypesTable(new());
        }

        internal CraftingPlannerData(ItemRepository itemsTable, ModsTable modsTable, ItemTypesTable itemTypeTable)
        {
            m_items = itemsTable;
            m_mods = modsTable;
            m_itemTypes = itemTypeTable;
        } 
    }
}
