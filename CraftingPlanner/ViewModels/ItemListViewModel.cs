using CraftingPlanner.Models;
using CraftingPlanner.UI;
using CraftingPlanner.ViewModels.Filters;
using CraftingPlannerLib;
using CraftingPlannerLib.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CraftingPlanner.ViewModels
{
    internal partial class ItemListViewModel : ViewModelBase
    {
        private readonly ItemNameFilter m_itemNameFilter;
        private readonly ItemTypeFilter m_itemTypeFilter;
        private readonly ModFilter m_modFilter;

        private readonly IEnumerable<IItemFilter> m_itemFilters;

        private IEnumerable<OutputType> m_outputTypes;
        private IEnumerable<Item> m_filteredItems;
        private Item? m_selectedItem;

        public ICommand FilterCommand { get; }

        public ICommand ClearFilterCommand { get; }

        public ICommand FindIngredientCommand { get; }

        public IEnumerable<Mod> Mods
            => Data.Mods.GetAll();

        public IEnumerable<ItemType> ItemTypes
            => Data.ItemTypes.GetAll();

        public IEnumerable<Item> FilteredItems
        {
            get
            {
                return m_filteredItems.OrderBy(x=>x.Name);
            }
            set
            {
                base.SetProperty(ref m_filteredItems, value);
            }
        }

        public IEnumerable<OutputType> OutputTypes
        {
            get
            {
                return m_outputTypes;
            }
            set
            {
                base.SetProperty(ref m_outputTypes, value);
            }
        }

        public Item? SelectedItem
        {
            get
            {
                return m_selectedItem;
            }
            set
            {
                base.SetProperty(ref m_selectedItem, value);
            }
        }

        public CraftingPlannerData Data { get; private set; }

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

        public ItemListViewModel()
        {
            m_itemNameFilter = new();
            m_itemTypeFilter = new();
            m_modFilter = new();

            m_itemFilters = new List<IItemFilter>()
            {
                m_itemTypeFilter,
                m_modFilter,
                m_itemNameFilter,
            };

            FilterCommand = new RelayCommand(OnFilter);
            ClearFilterCommand = new RelayCommand(OnFilterClear);
            FindIngredientCommand = new RelayCommand(ShowItemAsIngredient);

            Data = new CraftingPlannerData();
            m_filteredItems = Data.Items.GetAll();
            FilteredItems = Data.Items.GetAll();
            m_outputTypes = Enum.GetValues<OutputType>();
        }

        public void SetData(CraftingPlannerData data)
        {
            Data = data;
            base.OnPropertyChanged(nameof(Mods));
            base.OnPropertyChanged(nameof(ItemTypes));
            m_filteredItems = Data.Items.GetAll();
        }

        private void OnFilter(object? obj)
            => OnFilterChanged();

        private void OnFilterClear(object? obj)
        {
            base.SetProperty(ref m_itemNameFilter.ItemName, null, nameof(ItemNameFilter));
            base.SetProperty(ref m_itemTypeFilter.ItemType, null, nameof(ItemTypeFilter));
            base.SetProperty(ref m_modFilter.Mod, null, nameof(ModFilter));
            FilteredItems = Data.Items.GetAll();
        }

        private void OnFilterChanged()
        {
            FilteredItems = Data.Items.Filter(m_itemFilters);
        }

        private void ShowItemAsIngredient(object? obj)
        {
            if (SelectedItem == null)
            {
                return;
            }

            var allItems = Data.Items.GetAll().Where(x => x.Recipe != null && x.Recipe.HasItemAsIngredient(SelectedItem));
            foreach (var filter in m_itemFilters)
                allItems = filter.Filter(allItems);

            FilteredItems = allItems;
        }
    }
}
