using CraftingPlanner.ViewModels;
using CraftingPlannerLib.DataImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            // Test
            var path = "D:\\Documents\\GTNH Recipes.json";
            var importer = new DataImporter();
            var result = await importer.Import(path);

            var vm = new ItemListViewModel();
            this.ItemListControl.DataContext = vm;
            vm.SetData(result);
        }
    }
}
