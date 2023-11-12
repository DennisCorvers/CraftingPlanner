using DataImport;
using DataImport.RecipeExporter;

namespace CraftingPlannerLib.RecipeDB
{
    public class RecipeDatabase
    {
        public IItemRepository ItemRepository { get; }

        public IModRepository ModRepository { get; }

        public IRecipeRepository RecipeRepository { get; }

        public static async Task<RecipeDatabase> Create(string filePath)
        {
            IDataImporter importer = new RecipeExporterImporter();
            var data = await importer.Import(filePath);

            return new RecipeDatabase(data);
        }

        public static Task<RecipeDatabase> Create(DataImport.RecipeDB recipeDB)
            => Task.FromResult(new RecipeDatabase(recipeDB));

        private RecipeDatabase(DataImport.RecipeDB recipeDB)
        {
            ItemRepository = new ItemRepository(recipeDB.Items);
            ModRepository = new ModRepository(recipeDB.Mods);
            RecipeRepository = new RecipeRepository(recipeDB.Recipes);
        }
    }
}
