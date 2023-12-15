using CraftingPlanner.Presentation.ViewModels.RecipeImport;
using CraftingPlannerLib.RecipeDB;
using CraftingPlannerLib.RecipeDB.Models;
using DataImport;
using DataImport.RecipeExporter;
using System;
using System.Linq;
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

        private async Task<IImportedRecipesDb> Test()
        {
            var path = "D:\\Documents\\temp\\stoneblock.json";
            var importer = new RecipeExporterImporter();

            var itemdb = await ImportedRecipesDb.Create(importer, path);
            return itemdb;
        }
    }
}
