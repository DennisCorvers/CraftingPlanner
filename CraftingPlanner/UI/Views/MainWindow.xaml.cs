using CraftingPlanner.UI.ViewModels.RecipeImport;
using CraftingPlannerLib.RecipeDB;
using DataImport;
using DataImport.RecipeExporter;
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
            this.ItemListControl.DataContext = new RecipeListViewModel(data);
        }

        private async Task<ImportedRecipesDb> Test()
        {
            var path = "D:\\Documents\\temp\\stoneblock.json";
            var importer = new RecipeExporterImporter();

            var itemdb = await ImportedRecipesDb.Create(importer, path);
            return itemdb;
        }
    }
}
