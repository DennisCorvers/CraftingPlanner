using CraftingPlannerLib.RecipeDB;
using DataImport.Models;
using System;
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

        private FilterProperty<Mod> m_selectedModFilter;
        private FilterProperty<string> m_itemNameFilter;
        private FilterProperty<ItemStackType> m_itemStackFilter;

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

        public IReadOnlyList<Mod> Mods
        { get; }

        public IReadOnlyList<ItemStackType> ItemStackTypes
        { get; }

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
                return m_selectedModFilter.Value;
            }
            set
            {
                if (m_selectedModFilter.SetValue(value))
                    base.OnPropertyChanged();
            }
        }

        public string? ItemNameFilter
        {
            get
            {
                return m_itemNameFilter.Value;
            }
            set
            {
                if (m_itemNameFilter.SetValue(value))
                    base.OnPropertyChanged();
            }
        }

        public ItemStackType ItemStackFilter
        {
            get
            {
                return m_itemStackFilter.Value;
            }
            set
            {
                if (m_itemStackFilter.SetValue(value))
                    base.OnPropertyChanged();
            }
        }

        public ICommand FilterCommand { get; }

        public ICommand ClearFilterCommand { get; }

        internal RecipeListViewModel(IImportedRecipesDb importedRecipes)
        {
            m_itemStackFilter = new FilterProperty<ItemStackType>(ItemStackType.Output);

            m_importedRecipesDb = importedRecipes;
            m_viewedRecipes = importedRecipes.RecipeService
                .GetAll()
                .ToArray();

            Mods = importedRecipes.ModService
                .GetMods()
                .ToArray();

            ItemStackTypes = (ItemStackType[])Enum.GetValues(typeof(ItemStackType))
                .Cast<ItemStackType>();

            FilterCommand = new RelayCommand(OnFilter);
            ClearFilterCommand = new RelayCommand(OnClearFilter);
        }

        protected virtual void OnFilter(object? obj)
        {
            if (m_selectedModFilter.IsDefault && m_itemNameFilter.IsDefault && m_itemStackFilter.IsDefault)
                return;

            var filteredItems = m_importedRecipesDb.ItemService
                .Find(ItemNameFilter, SelectedModFilter);

            ViewedRecipes = m_importedRecipesDb.RecipeService
                .FindAll(filteredItems, ItemStackFilter)
                .ToArray();
        }

        protected virtual void OnClearFilter(object? obj)
        {
            SelectedModFilter = m_selectedModFilter.DefaultValue;
            ItemNameFilter = m_itemNameFilter.DefaultValue;
            ItemStackFilter = m_itemStackFilter.DefaultValue;

            ViewedRecipes = m_importedRecipesDb.RecipeService
                .GetAll()
                .ToArray();
        }
    }
}
