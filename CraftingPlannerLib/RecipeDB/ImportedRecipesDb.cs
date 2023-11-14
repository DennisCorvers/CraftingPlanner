using CraftingPlannerLib.RecipeDB.Services;
using DataImport;

namespace CraftingPlannerLib.RecipeDB
{

    public class ImportedRecipesDb : IImportedRecipesDb
    {
        public ItemService ItemService { get; }

        public ModService ModService { get; }

        public RecipeService RecipeService { get; }

        private ImportedRecipesDb(RecipesImport importedData)
        {
            var itemRepo = new ItemRepository(importedData.Items);
            var modRepo = new ModRepository(importedData.Mods);
            var recipeRepo = new RecipeRepository(importedData.Recipes);

            ItemService = new ItemService(itemRepo);
            ModService = new ModService(modRepo);
            RecipeService = new RecipeService(modRepo, itemRepo, recipeRepo);
        }

        public static async Task<ImportedRecipesDb> Create(IDataImporter importer, string filePath)
        {
            var data = await importer.Import(filePath);
            return new ImportedRecipesDb(data);
        }
    }
}
