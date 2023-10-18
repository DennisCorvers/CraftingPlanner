using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CraftingPlannerLib.Utils;
using ProtoBuf;

namespace CraftingPlannerLib.Entities
{
    [Serializable]
    [DebuggerDisplay("Name = {Name}")]
    [ProtoContract]
    public class Item : NamedEntity
    {
        private ItemType? m_type;
        private Mod? m_mod;
        private Recipe? m_recipe;

        public ItemType? Type
        {
            get => m_type;
            set => m_type = value;
        }

        public Mod? Mod
        {
            get => m_mod;
            set => m_mod = value;
        }

        public Recipe? Recipe
        {
            get => m_recipe;
            set => m_recipe = value;
        }

        public Item(string itemName)
            : this(itemName, null, null, null)
        {

        }

        public Item(string itemName, ItemType? type, Mod? mod, Recipe? recipe)
            : base(itemName)
        {
            Type = type;
            Mod = mod;
            Recipe = recipe;
        }

        #region Equality
        //protected virtual Type EqualityContract
        //    => typeof(Item);

        //public static bool operator !=(Item? left, Item? right)
        //{
        //    return !(left == right);
        //}

        //public static bool operator ==(Item? left, Item? right)
        //{
        //    if (Equals(left, right))
        //        return true;

        //    return !Equals(left, null) && left.Equals(right);
        //}

        //public override bool Equals(object? obj)
        //{
        //    return Equals(obj as Item);
        //}

        //public virtual bool Equals(Item? other)
        //{
        //    if (Equals(this, other))
        //        return true;

        //    if (Equals(other, null))
        //        return false;

        //    return EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(Id);
        //}
        #endregion
    }
}
