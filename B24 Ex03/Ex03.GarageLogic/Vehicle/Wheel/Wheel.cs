using Ex03.GarageLogic.DTO;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.Vehicles
{
    internal class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;
        private const double k_RatioPSIBAR = 14.5038;
        private const float k_ZeroAirPressure = 0f;

        internal Wheel(float i_MaxAirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;
        }

        internal string ManufacturerName
        {
            private get { return m_ManufacturerName; }

            set { m_ManufacturerName = value; }
        }

        internal float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
        }

        internal float CurrentAirPressure
        {
            private get { return m_CurrentAirPressure; }

            set
            {
                if (value > m_MaxAirPressure || value < k_ZeroAirPressure)
                {
                    validateNotNegativeAirPressure(value);
                    throw new ValueOutOfRangeException("Air pressure", k_ZeroAirPressure, m_MaxAirPressure);
                }

                m_CurrentAirPressure = value;
            }
        }

        private void validateNotNegativeAirPressure(float i_AdditionalAirPressure)
        {
            if (i_AdditionalAirPressure < k_ZeroAirPressure)
            {
                throw new ArgumentException("Cannot accept negative value");
            }
        }

        internal void InflateTire(float i_AdditionalAirPressure)
        {
            validateNotNegativeAirPressure(i_AdditionalAirPressure);
            CurrentAirPressure += i_AdditionalAirPressure;
        }

        internal void InflateTireToMax()
        {
            float deltaToMax = m_MaxAirPressure - m_CurrentAirPressure;
            InflateTire(deltaToMax);
        }

        internal List<FieldClassInfo> GetFieldsInfo()
        {
            return new List<FieldClassInfo>
            {
                new FieldClassInfo("ManufacturerName", "Please enter wheels manufacturor name: "),
                new FieldClassInfo("CurrentAirPressure", "Please enter wheels current air pressure: ", FieldClassInfo.eInputType.Float)
            };
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Manufacturer: {ManufacturerName}");
            stringBuilder.AppendLine($"Current Air Pressure: {CurrentAirPressure} PSI ({(double)CurrentAirPressure / k_RatioPSIBAR} BAR)");
            stringBuilder.AppendLine($"Max Pressure: {MaxAirPressure} PSI ({(double)MaxAirPressure / k_RatioPSIBAR} BAR)");

            return stringBuilder.ToString();
        }
    }
}