using DataImport.EqualityComparers;
using System.Data;
using System.Text;

namespace DataImport.RecipeExporter
{
    public class RecipeExporterImporter : IDataImporter
    {
        public async Task<RecipesImport> Import(string path)
        {
            var importData = await ReadAsync<ExportData>(path);

            if (importData == null)
                throw new ArgumentException($"Unable to deserialize file: {path}");

            var indexes = BuildIndexes(importData);

            var itemLookup = new Func<int, Models.Item>(x => indexes.ItemIndex[x]);
            var recipes = importData.Recipes.Select(x => MapRecipe(x, itemLookup));

            return new RecipesImport(
                recipes.ToList(),
                indexes.ModIndex.Values,
                indexes.ItemIndex.Values);
        }

        private static Models.Recipe MapRecipe(Recipe recipe, Func<int, Models.Item> itemLookup)
        {
            var output = MapItemStack(recipe.Output, itemLookup);
            var input = recipe.Input.Select(x => MapItemStack(x, itemLookup));

            return new Models.Recipe(input, output);
        }

        private static Models.ItemStack MapItemStack(ItemStack itemStack, Func<int, Models.Item> itemLookup)
        {
            if (itemStack is AlternativeItemStack aItemStack)
            {
                var alternativeItems = aItemStack.AlternativeItemIds.Select(x => itemLookup(x));
                return new Models.AlternativeItemStack(
                    itemLookup(aItemStack.ItemID),
                    aItemStack.Amount,
                    alternativeItems);
            }
            else
            {
                return new Models.ItemStack(
                    itemLookup(itemStack.ItemID),
                    itemStack.Amount);
            }
        }

        private static Indexes BuildIndexes(ExportData data)
        {
            // Create intermediate object to link items to mods after item indexing.
            var modLinkerIndex = data.ModLookup.ToDictionary(
                k => k.Key,
                v => new ModLinker(v.Key, v.Value));

            var modLookup = new Func<int, Models.Mod>(x => modLinkerIndex[x].Mod);

            // Index all items.
            var itemIndex = data.ItemLookup.ToDictionary(
                k => k.Key,
                v => new Models.Item(v.Key, v.Value.Name, modLookup(v.Value.ModID)));

            var itemGrouping = itemIndex.Values.ToLookup(x => x.Mod.ID);

            // Link items to mods.
            foreach (var modLinker in modLinkerIndex.Values)
                modLinker.LinkItems(itemGrouping[modLinker.Mod.ID]);

            var modIndex = modLinkerIndex.ToDictionary(
                k => k.Key,
                v => v.Value.Mod);

            return new Indexes(modIndex, itemIndex);
        }

        private class ModLinker
        {
            public Models.Mod Mod { get; }

            private HashSet<Models.Item> items { get; }

            public ModLinker(int id, string modName)
            {
                items = new HashSet<Models.Item>(BaseModelComparer.Default);
                Mod = new Models.Mod(id, modName, items);
            }

            public void LinkItems(IEnumerable<Models.Item> items)
            {
                if (items.Select(this.items.Add).Any(x => false))
                    throw new InvalidOperationException("Mod can't have the same item twice.");
            }
        }

        private readonly struct Indexes
        {
            public IDictionary<int, Models.Mod> ModIndex { get; }

            public IDictionary<int, Models.Item> ItemIndex { get; }

            public Indexes(IDictionary<int, Models.Mod> modIndex, IDictionary<int, Models.Item> itemIndex)
            {
                ModIndex = modIndex;
                ItemIndex = itemIndex;
            }
        }

        private static async Task<T?> ReadAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            using StreamReader reader = new StreamReader(stream, Encoding.ASCII);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());
        }
    }
}
