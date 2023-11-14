using CraftingPlanner.ViewModels;
using CraftingPlannerLib.RecipeDB;
using DataImport;
using DataImport.RecipeExporter;
using System.Linq;
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

            Test();
        }

        private async void Test()
        {
            var path = "D:\\Documents\\temp\\stoneblock.json";
            var importer = new RecipeExporterImporter();

            var itemdb = await ImportedRecipesDb.Create(importer, path);

            var result = itemdb.RecipeService.FindAll("iron ingot", ItemStackType.Output);
        }

        private async void Test2()
        {
            var path = "D:\\Documents\\temp\\2023-11-08--13-51.json";
            var path2 = "D:\\Documents\\temp\\stoneblock.json";

            var importer = new RecipeExporterImporter();

            var newfile = await importer.Import(path);

            var a = newfile.Recipes.Where(x=>x.Input.Sum(y=>y.Amount) > 9).ToList();
        }
    }
}
