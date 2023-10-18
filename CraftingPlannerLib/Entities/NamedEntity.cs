using CraftingPlannerLib.Utils;
using System.Diagnostics;

namespace CraftingPlannerLib.Entities
{
    [DebuggerDisplay("Name = {Name}")]
    public abstract class NamedEntity
    {
        private int m_id;
        private string m_name;

        public int Id
        {
            get => m_id;
            set => m_id = value;
        }

        public string Name
        {
            get => m_name;
            set => m_name = value.ToFirstLetterUpperCase();
        }

        public NamedEntity(string name)
            : this(-1, name)
        { }

        public NamedEntity(int id, string name)
        {
            m_id = id;
            m_name = name.ToFirstLetterUpperCase();
        }
    }
}
