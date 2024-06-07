﻿using CraftingPlannerData.Repositories;
using CraftingPlannerLib.RecipeDB;
using CraftingPlannerLib.RecipeDB.Services;
using DataImport;

namespace CraftingPlannerData
{
    public class ImportedRecipesDb : IImportedRecipesDb
    {
        public ItemService ItemService { get; }

        public ModService ModService { get; }

        public RecipeService RecipeService { get; }

        private ImportedRecipesDb(ItemService itemService, ModService modService, RecipeService recipeService)
        {
            ItemService = itemService;
            ModService = modService;
            RecipeService = recipeService;
        }

        public static async Task<IImportedRecipesDb> Create(IDataImporter importer)
        {
            var data = await importer.Import();

            var itemRepo = new ItemRepository(data.Items);
            var modRepo = new ModRepository(data.Mods);
            var recipeRepo = new RecipeRepository(data.Recipes);

            return new ImportedRecipesDb(
                new ItemService(itemRepo),
                new ModService(modRepo),
                new RecipeService(modRepo, itemRepo, recipeRepo));
        }
    }
}
