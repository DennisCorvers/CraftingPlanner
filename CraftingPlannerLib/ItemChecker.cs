using CraftingPlannerLib.Entities;
using CraftingPlannerLib.Utils;
using Microsoft.VisualBasic;
using ProtoBuf;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace CraftingPlannerLib
{
//    [Serializable]
//    [ProtoContract]
//    public class ItemChecker
//    {
//        public IEnumerable<Item> ItemList
//            => m_items.Values;
//        public IEnumerable<string> Mods
//            => m_modpacks;
//        public IEnumerable<string> Types
//            => m_itemTypes;

//        private HashSet<string> m_mods
//            => m_modpacks;

//        private readonly HashSet<string> m_itemNameLookup;

//#pragma warning disable IDE0044 // Readonly modifier not possible with ProtoBuf
//        [ProtoMember(1)]
//        private HashSet<string> m_modpacks = new();
//        [ProtoMember(2)]
//        private Dictionary<int, Item> m_items = new();
//        [ProtoMember(3)]
//        private HashSet<string> m_itemTypes = new();
//        [ProtoMember(4)]
//        private int m_lastItemId = 0;
//#pragma warning restore IDE0044

//        public ItemChecker()
//        {
//            m_itemNameLookup = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
//        }

//        [OnDeserialized]
//        private void OnDeserialized(StreamingContext context)
//        {
//            // Assemble item name lookup table
//            foreach (var item in m_items)
//            {
//                item.Value.OnDeserializing(this);
//                m_itemNameLookup.Add(item.Value.ItemName);
//            }
//        }

//        public bool AddNewType(string type)
//        {
//            if (m_itemTypes.Contains(type))
//            {
//                return false;
//            }
//            m_itemTypes.Add(type.ToFirstLetterUpperCase());
//            return true;
//        }

//        public bool DeleteType(string type)
//        {
//            if (m_itemTypes.Contains(type))
//            {
//                m_itemTypes.Remove(type);

//                foreach (var item in m_items.Values)
//                {
//                    if (item.Type == type)
//                        item.Type = null;
//                }
//                return true;
//            }
//            return false;
//        }

//        public bool AddNewMod(string mod)
//        {
//            if (Mods.Contains(mod))
//            {
//                return false;
//            }

//            m_mods.Add(mod.ToFirstLetterUpperCase());
//            return true;
//        }

//        public bool DeleteMod(string mod)
//        {
//            if (Mods.Contains(mod))
//            {
//                m_mods.Remove(mod);

//                foreach (var item in m_items.Values)
//                {
//                    if (item.ModName == mod)
//                        item.ModName = null;
//                }

//                return true;
//            }
//            return false;
//        }

//        public bool AddNewItem(Item item)
//        {
//            if (m_itemNameLookup.Contains(item.ItemName))
//                return false;

//            item.ItemID = m_lastItemId;
//            m_items.Add(m_lastItemId, item);

//            m_itemNameLookup.Add(item.ItemName);

//            m_lastItemId++;
//            return true;
//        }

//        public bool DeleteItem(int itemId)
//        {
//            if (m_items.TryGetValue(itemId, out Item? value))
//            {
//                m_itemNameLookup.Remove(value.ItemName);
//                m_items.Remove(itemId);

//                foreach (var item in m_items.Values)
//                    item.Recipe.Remove(value);
//            }

//            return value is not null;
//        }

//        public bool UpdateItem(Item newItem, Item oldItem)
//        {
//            // Item has been renamed
//            if (!string.Equals(newItem.ItemName, oldItem.ItemName, StringComparison.OrdinalIgnoreCase))
//            {
//                // New item name already exists
//                if (m_itemNameLookup.Contains(newItem.ItemName))
//                    return false;

//                // Rename in the item lookup
//                m_itemNameLookup.Remove(oldItem.ItemName);
//                m_itemNameLookup.Add(newItem.ItemName);
//            }

//            oldItem.CopyFrom(newItem);

//            return true;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Item FindItem(int itemId)
//        {
//            return m_items[itemId];
//        }

//        public IEnumerable<Item> FindItems(string? itemname = null, string? type = null, string? mod = null, Dictionary<Item, double>? craftingneed = null)
//        {
//            IEnumerable<Item> result = m_items.Values;

//            if (!string.IsNullOrWhiteSpace(itemname))
//                result = result.Where(x => x.ItemName.Contains(itemname, StringComparison.OrdinalIgnoreCase));

//            if (!string.IsNullOrWhiteSpace(type))
//                result = result.Where(x => x.Type == type);

//            if (!string.IsNullOrWhiteSpace(mod))
//                result = result.Where(x => x.ModName == mod);

//            if (craftingneed != null)
//                result = result.Where(x => x.Recipe == craftingneed);

//            return result;
//        }

//        public IEnumerable<Item> FilterItems(IEnumerable<IItemFilter> itemFilters)
//        {
//            IEnumerable<Item> results = m_items.Values;

//            foreach (var filter in itemFilters)
//                results = filter.Filter(results);

//            return results;
//        }
//    }
}
