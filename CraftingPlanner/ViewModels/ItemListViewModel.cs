using CraftingPlanner.Models;
using CraftingPlanner.UI;
using CraftingPlanner.ViewModels.Filters;
using CraftingPlannerLib;
using CraftingPlannerLib.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CraftingPlanner.ViewModels
{
    internal partial class ItemListViewModel : ViewModelBase
    {
        private ItemNameFilter m_itemNameFilter;
        private ItemTypeFilter m_itemTypeFilter;
        private ModFilter m_modFilter;
        private OutputTypeFilter m_outTypeFilter;

        public IEnumerable<IItemFilter> ItemFilters { get; }

        public ICommand FilterCommand { get; }

        public ICommand ClearFilterCommand { get; }

        public IEnumerable<Item> Items { get; }

        public Item? SelectedItem { get; set; }

        public CraftingPlannerData? Data { get; set; }

        public string? ItemNameFilter
        {
            get
            {
                return m_itemNameFilter.ItemName;
            }
            set
            {
                if (base.SetProperty(ref m_itemNameFilter.ItemName, value))
                    OnFilterChanged();
            }
        }

        public ItemType? ItemTypeFilter
        {
            get
            {
                return m_itemTypeFilter.ItemType;
            }
            set
            {
                if (base.SetProperty(ref m_itemTypeFilter.ItemType, value))
                    OnFilterChanged();
            }
        }

        public Mod? ModFilter
        {
            get
            {
                return m_modFilter.Mod;
            }
            set
            {
                if (base.SetProperty(ref m_modFilter.Mod, value))
                    OnFilterChanged();
            }
        }

        public OutputType FilterOptions
        {
            get
            {
                return m_outTypeFilter.Type;
            }
            set
            {
                if (base.SetProperty(ref m_outTypeFilter.Type, value))
                    OnFilterChanged();
            }
        }

        public ItemListViewModel()
        {
            m_itemNameFilter = new();
            m_itemTypeFilter = new();
            m_modFilter = new();
            m_outTypeFilter = new();

            ItemFilters = new List<IItemFilter>()
            {
                m_itemTypeFilter,
                m_modFilter,
                m_itemNameFilter,
                m_outTypeFilter
            };

            FilterCommand = new RelayCommand(OnFilter);
            ClearFilterCommand = new RelayCommand(OnFilterClear);

            Items = Array.Empty<Item>();
        }

        private void OnFilter(object? obj)
        {

        }

        private void OnFilterClear(object? obj)
        {
            m_itemNameFilter.ItemName = null;
            m_itemTypeFilter.ItemType = null;
            m_modFilter.Mod = null;
            m_outTypeFilter.Type = OutputType.Output;
        }

        private void OnFilterChanged()
        {

        }
    }
}
