using CraftingPlannerLib.RecipeDB;
using DataImport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CraftingPlanner.UI.ViewModels.RecipeImport
{
    internal class RecipeListViewModel : BaseViewModel
    {
        private IImportedRecipesDb m_importedRecipesDb;
        private IReadOnlyList<Recipe> m_viewedRecipes;
        private Recipe? m_selectedRecipe;

        private Mod? m_selectedModFilter;
        private string? m_itemNameFilter;
        private ItemStackType m_itemStackFilter;

        public IReadOnlyList<Recipe> ViewedRecipes
        {
            get
            {
                return m_viewedRecipes;
            }
            set
            {
                base.SetProperty(ref m_viewedRecipes, value);
            }
        }

        public Recipe? SelectedRecipe
        {
            get
            {
                return m_selectedRecipe;
            }
            set
            {
                base.SetProperty(ref m_selectedRecipe, value);
            }
        }

        public Mod? SelectedModFilter
        {
            get
            {
                return m_selectedModFilter;
            }
            set
            {
                base.SetProperty(ref m_selectedModFilter, value);
            }
        }

        public string? ItemNameFilter
        {
            get
            {
                return m_itemNameFilter;
            }
            set
            {
                base.SetProperty(ref m_itemNameFilter, value);
            }
        }

        public ItemStackType ItemStackFilter
        {
            get
            {
                return m_itemStackFilter;
            }
            set
            {
                base.SetProperty(ref m_itemStackFilter, value);
            }
        }

        public ICommand FilterCommand { get; }

        public ICommand ClearFilterCommand { get; }

        internal RecipeListViewModel(IImportedRecipesDb importedRecipes)
        {
            m_importedRecipesDb = importedRecipes;
            m_viewedRecipes = importedRecipes.RecipeService
                .GetAll()
                .ToArray();

            FilterCommand = new RelayCommand(OnFilter);
            ClearFilterCommand = new RelayCommand(OnClearFilter);
        }

        protected virtual void OnFilter(object? obj)
        {
            var filteredItems = m_importedRecipesDb.ItemService
                .Find(ItemNameFilter, SelectedModFilter);

            ViewedRecipes = m_importedRecipesDb.RecipeService
                .FindAll(filteredItems, ItemStackFilter)
                .ToArray();
        }

        protected virtual void OnClearFilter(object? obj)
        {
            SelectedModFilter = null;
            ItemNameFilter = null;
            ItemStackFilter = ItemStackType.Output;

            ViewedRecipes = m_importedRecipesDb.RecipeService
                .GetAll()
                .ToArray();
        }
    }
}
