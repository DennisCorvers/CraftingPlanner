using DataImport.Models;

namespace CraftingPlanner.Presentation.ViewModels.RecipeImport
{
    internal class RecipeDetailViewModel : BaseViewModel
    {
        private Recipe? m_selectedRecipe;

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
    }
}
