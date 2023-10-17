using CraftingPlannerLib.Tables;

namespace CraftingPlannerLib
{
    public class CraftingPlannerData
    {
        private ItemsTable m_items;

        private ModsTable m_mods;

        private ItemTypesTable m_itemTypes;

        public CraftingPlannerData()
        {
            m_items = new ItemsTable(new());
            m_mods = new ModsTable(new());
            m_itemTypes = new ItemTypesTable(new());
        }

        internal CraftingPlannerData(ItemsTable itemsTable, ModsTable modsTable, ItemTypesTable itemTypeTable)
        {
            m_items = itemsTable;
            m_mods = modsTable;
            m_itemTypes = itemTypeTable;
        }
    }
}
