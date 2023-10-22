using CraftingPlannerLib.DataImport.Models;
using CraftingPlannerLib.Tables;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CraftingPlannerLib.DataImport
{
    public class DataImporter
    {
        public async Task<CraftingPlannerData> Import(string filePath)
        {
            var data = await ReadAsync<ExportData>(filePath);

            if (data == null)
                return new CraftingPlannerData();

            var modsTable = new ModsTable(data.Mods.ToDictionary(
                k => k.ID,
                v => CreateMod(v)));

            var itemTypeTable = new ItemTypesTable(data.ItemTypes.ToDictionary(
                k => k.ID,
                v => CreateItemType(v)));

            var itemsTable = new ItemRepository(data.Items.ToDictionary(
                k => k.ID,
                v => CreateItem(v)));

            // Add recipes to items.
            foreach (var itemEntity in data.Items)
                LinkRecipeToItem(itemEntity, itemsTable.Get!);

            return new CraftingPlannerData(itemsTable, modsTable, itemTypeTable);

            Entities.Item CreateItem(External.Item other)
            {
                return new Entities.Item(
                    other.Name,
                    other.TypeID == -1 ? null : itemTypeTable!.Get(other.TypeID)!,
                    other.ModID == -1 ? null : modsTable!.Get(other.ModID),
                    null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LinkRecipeToItem(External.Item item, Func<int, Entities.Item> itemLookup)
        {
            // Imported item has no recipe. Can skip.
            if (item.Recipes == null || item.Recipes.Count == 0)
                return;

            // Convert imported recipe to new recipe.
            var targetItem = itemLookup(item.ID);
            var inputItems = item.Recipes.ToDictionary(k => itemLookup(k.Key), v => v.Value);
            var recipe = new Entities.Recipe(targetItem, 1, inputItems);

            // Link recipe to item.
            targetItem.Recipe = recipe;
        }

        private static Entities.Mod CreateMod(External.Mod other)
            => new(other.Name) { Id = other.ID };

        private static Entities.ItemType CreateItemType(External.ItemType other)
            => new(other.Name) { Id = other.ID };

        private static async Task<T?> ReadAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
}
