using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ProtoBuf;

namespace CraftingPlannerLib.Entities
{
    [Serializable]
    [DebuggerDisplay("Name = {ItemName}")]
    [ProtoContract]
    public class Item : Entity
    {
        public string ItemName { get; set; }

        public ItemType? Type { get; set; }

        public Mod? Mod { get; set; }

        public IReadOnlyDictionary<Item, double>? Recipe { get; set; }

        public Item(string itemName)
            : this(itemName, null, null, null)
        {

        }

        public Item(string itemName, ItemType? type, Mod? mod, IReadOnlyDictionary<Item, double>? recipe)
        {
            ItemName = itemName;
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
