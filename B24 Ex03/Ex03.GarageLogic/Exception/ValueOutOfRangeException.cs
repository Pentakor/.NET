using System;

namespace Ex03.GarageLogic.Exceptions
{
    internal class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;

        internal float MinValue
        {
            get { return m_MinValue; }
            set { m_MinValue = value; }
        }

        internal float MaxValue
        {
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }

        internal ValueOutOfRangeException(string i_ParameterName, float i_MinValue, float i_MaxValue)
            : base($"{i_ParameterName} is out of range. Minimum: {i_MinValue}, Maximum: {i_MaxValue}")
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}
