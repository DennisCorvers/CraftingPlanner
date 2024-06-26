﻿using CraftingPlanner.Presentation.ViewModels.RecipeImport;
using CraftingPlannerData;
using CraftingPlannerLib.RecipeDB.Models;
using DataImport.RecipeExporter;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CraftingPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        private async void Initialize()
        {
            var data = await Test();

            var recipeListVM = new RecipeListViewModel(data);
            var detailVM = new RecipeDetailViewModel();

            recipeListVM.SelectedRecipeChanged += new Action<RecipeGrouping?>((x) =>
            {
                detailVM.Recipes = x?.Recipes!;
            });

            this.ItemListControl.DataContext = recipeListVM;
            this.RecipeDetailControl.DataContext = detailVM;
        }

        private async Task<IRecipeDB> Test()
        {
            var path = "D:\\Documents\\temp\\stoneblock.json";
            var importer = new RecipeExporterImporter(path);

            var itemdb = await ImportedRecipesDb.Create(importer);
            return itemdb;
        }
    }
}
