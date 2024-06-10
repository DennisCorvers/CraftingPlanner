using CraftingPlannerLib.Data.GameData;
using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.Data.UserData
{
    public interface ICustomItemRepository : IItemRepository
    {
        ItemMetaData? GetItemMetaData(Item item);

        void AddItem(Item item);

        void UpdateItem(int id, Item item);

        void DeleteItem(int id);
    }
}
