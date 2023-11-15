using System.Collections.Generic;

namespace CraftingPlanner.Presentation
{
    internal struct FilterProperty<T>
    {
        private T? m_value;

        public T? Value
        {
            get => m_value;
            set => m_value = value;
        }

        public T? DefaultValue { get; }

        public bool IsDefault
            => EqualityComparer<T>.Default.Equals(m_value, DefaultValue);

        public FilterProperty(T? defaultValue)
        {
            m_value = defaultValue;
            DefaultValue = defaultValue;
        }

        public void Reset()
            => Value = DefaultValue;

        public bool SetValue(T? newValue)
        {
            if (!EqualityComparer<T>.Default.Equals(Value, newValue))
            {
                Value = newValue;
                return true;
            }

            return false;
        }
    }
}
