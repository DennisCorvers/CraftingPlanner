using CraftingPlannerLib.Utils;
using System.Diagnostics;

namespace CraftingPlannerLib.Entities
{
    [DebuggerDisplay("Name = {Name}")]
    public abstract class NamedEntity
    {
        private const int DefaultID = -1;
        private int m_id;
        private string m_name;

        public int Id
        {
            get => m_id;
            internal set
            {
                if (value == DefaultID)
                    throw new ArgumentException(nameof(value));

                m_id = value;
            }
        }

        public string Name
        {
            get => m_name;
            set => m_name = value.ToFirstLetterUpperCase();
        }

        public NamedEntity(string name)
            : this(DefaultID, name)
        { }

        // Serialization constructor
        private NamedEntity(int id, string name)
        {
            m_id = id;
            m_name = name.ToFirstLetterUpperCase();
        }
    }
}
