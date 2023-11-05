using CraftingPlannerLib.DAL.Repositories;

namespace CraftingPlannerLib
{
    public class CraftingPlannerData
    {
        private ItemRepository m_items;

        private ModsTable m_mods;

        public ModsTable Mods
            => m_mods;
        public ItemRepository Items
            => m_items;

        public CraftingPlannerData()
        {
            m_items = new ItemRepository(new());
            m_mods = new ModsTable(new());
        }

        internal CraftingPlannerData(ItemRepository itemsTable, ModsTable modsTable)
        {
            m_items = itemsTable;
            m_mods = modsTable;
        } 
    }
}
