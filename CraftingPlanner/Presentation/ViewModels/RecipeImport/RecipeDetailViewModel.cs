using CraftingPlannerLib.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CraftingPlanner.Presentation.ViewModels.RecipeImport
{
    internal class RecipeDetailViewModel : BaseViewModel
    {
        private IReadOnlyList<Recipe> m_recipes;
        private int m_index;

        public int Count
            => m_recipes.Count;

        public int PageNumber
            => m_index + 1;

        public bool HasRecipes
            => Count > 0;

        public bool CanNavigate
            => Count > 1;

        public Recipe? Current
        {
            get
            {
                return m_index >= 0 && m_index < m_recipes.Count
                    ? m_recipes[m_index]
                    : default;
            }
        }

        public ICommand Next { get; }

        public ICommand Previous { get; }

        public IEnumerable<Recipe> Recipes
        {
            get { return m_recipes; }
            set
            {
                var oldValue = m_recipes;
                var newValue = value is IReadOnlyList<Recipe> list
                    ? list
                    : Array.Empty<Recipe>();

                if (base.SetProperty(ref m_recipes, newValue))
                    OnCollectionChanged(oldValue, newValue);
            }
        }

        public RecipeDetailViewModel()
        {
            m_index = 0;
            m_recipes = Array.Empty<Recipe>();

            Next = new RelayCommand(OnNext);
            Previous = new RelayCommand(OnPrevious);
        }

        protected virtual void OnIndexChanged()
        {
            base.OnPropertyChanged(nameof(PageNumber));
            base.OnPropertyChanged(nameof(Current));
        }

        /// <summary>
        /// Notifies collection-related fields of an update.
        /// </summary>
        protected virtual void OnCollectionChanged(IReadOnlyList<Recipe> oldValue, IReadOnlyList<Recipe> newValue)
        {
            // If index is already 0, force an update of the related fields.
            m_index = 0;
            OnIndexChanged();

            base.OnPropertyChanged(nameof(Count));
            base.OnPropertyChanged(nameof(HasRecipes));
            base.OnPropertyChanged(nameof(CanNavigate));
        }

        protected virtual void OnNext(object? obj)
        {
            m_index = (m_index + 1) % Count;
            OnIndexChanged();
        }

        protected virtual void OnPrevious(object? obj)
        {
            m_index = (m_index - 1 + Count) % Count;
            OnIndexChanged();
        }
    }
}
