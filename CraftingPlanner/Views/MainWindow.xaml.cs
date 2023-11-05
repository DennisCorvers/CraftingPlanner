using CraftingPlanner.ViewModels;
using DataImport;
using DataImport.RecipeExporter;
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
            var path = "D:\\Documents\\temp\\Stoneblock.json";
            var importer = new RecipeExporterImporter();

            await importer.Import(path);
        }

        private async void Test2()
        {
            var path = "D:\\Documents\\temp\\2023-11-02--23-17.json";
            var path2 = "D:\\Documents\\temp\\2023-11-02--23-17.json";
            //var path2 = "D:\\Projects\\Minecraft\\RecipeExporter\\run\\Recipe Exports\\2023-11-03--01-53.json";

            var importer = new RecipeExporterImporter();
            //await importer.Impoty2(path, path2);
        }
    }
}
