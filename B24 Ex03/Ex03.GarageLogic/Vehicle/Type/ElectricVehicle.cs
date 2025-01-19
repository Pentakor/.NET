using Ex03.GarageLogic.DTO;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.Vehicles.Types
{
    internal abstract class ElectricVehicle : Vehicle
    {
        private float m_RemainingBatteryTime;
        private float m_MaxBatteryTime;
        private const float k_ZeroBatteryTime = 0f;
        private const float k_MinutesInHour = 60f;

        internal float MaxBatteryTime
        {
            get { return m_MaxBatteryTime; }
        }

        internal float RemainingBatteryTime
        {
            get { return m_RemainingBatteryTime; }

            set
            {
                if (value / k_MinutesInHour > m_MaxBatteryTime || value / k_MinutesInHour < k_ZeroBatteryTime)
                {
                    if (m_RemainingBatteryTime > 0)
                    {
                        throw new ArgumentException($"You trying to add {value - (m_RemainingBatteryTime * k_MinutesInHour)} minutes, current battery time is {m_RemainingBatteryTime * k_MinutesInHour} minutes. " +
                            $"The maximum battery time is {m_MaxBatteryTime * k_MinutesInHour} minutes.");
                    }
                    else
                    {
                        throw new ValueOutOfRangeException("Battery time in minutes", k_ZeroBatteryTime, m_MaxBatteryTime * k_MinutesInHour);
                    }
                }

                m_RemainingBatteryTime = value / k_MinutesInHour;
                m_Info.AdjustPercentage(RemainingBatteryTime, m_MaxBatteryTime);
            }
        }

        internal void ChargeBatteryAndValidateNegativity(float i_AdditionalBatteryTime)
        {
            validateNoNegativeBatteryTime(i_AdditionalBatteryTime);
            RemainingBatteryTime = RemainingBatteryTime * k_MinutesInHour + i_AdditionalBatteryTime;
        }

        private void validateNoNegativeBatteryTime(float i_AdditionalBatteryTime)
        {
            if (i_AdditionalBatteryTime < k_ZeroBatteryTime)
            {
                throw new ArgumentException("Cannot accept negative value");
            }
        }

        protected internal void InitializeMaxBattery(float i_MaxBatteryTime)
        {
            m_MaxBatteryTime = i_MaxBatteryTime;
        }

        internal override List<FieldClassInfo> GetFieldsList()
        {
            List<FieldClassInfo> fieldsInfo = base.GetFieldsList();

            fieldsInfo.AddRange(new List<FieldClassInfo>
            {
                new FieldClassInfo("RemainingBatteryTime", "Please enter the number of minutes you would like to charge the vehicle: ", FieldClassInfo.eInputType.Float)
            });

            return fieldsInfo;
        }

        public sealed override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(base.ToString());
            stringBuilder.AppendLine("Battery information:");
            stringBuilder.AppendLine($"Battery time remaining: {m_RemainingBatteryTime} Hours");
            stringBuilder.AppendLine($"Maximum battery time capacity: {m_MaxBatteryTime} Hours");

            return stringBuilder.ToString();
        }
    }
}