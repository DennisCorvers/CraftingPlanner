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

            var mapper = new Mapping();
            var modsTable = new ModsTable(data.Mods.ToDictionary(k => k.ID, mapper.Map));
            var itemTypeTable = new ItemTypesTable(data.ItemTypes.ToDictionary(k => k.ID, mapper.Map));

            var itemsTable = new ItemsTable(data.Items.ToDictionary(k => k.ID, v =>
            {
                return new Entities.Item(
                    itemName: v.Name,
                    type: GetItemType(v.TypeID, itemTypeTable.Data),
                    mod: GetMod(v.ModID, modsTable.Data),
                    recipe: null);
            }));

            // Add recipes to items.
            foreach (var itemEntity in data.Items)
                itemsTable.Data[itemEntity.ID].Recipe = GetRecipe(itemEntity.Recipes, itemsTable.Data);

            return new CraftingPlannerData(itemsTable, modsTable, itemTypeTable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Entities.ItemType? GetItemType(int id, IDictionary<int, Entities.ItemType> map)
            => id == -1 ? null : map[id];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Entities.Mod? GetMod(int id, IDictionary<int, Entities.Mod> map)
            => id == -1 ? null : map[id];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Dictionary<Entities.Item, double>? GetRecipe(IDictionary<int, double>? recipe, IDictionary<int, Entities.Item> map)
        {
            if (recipe == null) return null;
            return recipe.ToDictionary(k => map[k.Key], k => k.Value);
        }

        private static async Task<T?> ReadAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
    }
}
