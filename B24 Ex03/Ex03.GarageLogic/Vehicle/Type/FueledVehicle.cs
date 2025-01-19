using Ex03.GarageLogic.DTO;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.Vehicles.Types
{
    internal abstract class FueledVehicle : Vehicle
    {
        private eFuelType m_FuelType;
        private float m_RemainingFuel;
        private float m_MaxFuelCapacity;
        private const float k_ZeroFuel = 0f;

        internal eFuelType FuelType
        {
            get { return m_FuelType; }
        }

        internal float MaxFuelCapacity
        {
            get { return m_MaxFuelCapacity; }
        }

        internal float RemainingFuel
        {
            get
            {
                return m_RemainingFuel;
            }

            set
            {
                if (value > m_MaxFuelCapacity || value < k_ZeroFuel)
                {
                    if (m_RemainingFuel > 0)
                    {
                        throw new ArgumentException($"You trying to add {value - RemainingFuel}L, current fuel amount is {m_RemainingFuel}L. " +
                            $"The maximum fuel capacity is {m_MaxFuelCapacity}L.");
                    }
                    else
                    {
                        throw new ValueOutOfRangeException("Fuel amount", k_ZeroFuel, m_MaxFuelCapacity);
                    }
                }

                m_RemainingFuel = value;
                m_Info.AdjustPercentage(RemainingFuel, m_MaxFuelCapacity);
            }
        }

        internal void AddFuelAndValidateType(float i_AdditionalFuel, string i_FuelType)
        {
            validateFuelType(i_FuelType);
            validateNoNegativeFuelAddition(i_AdditionalFuel);
            RemainingFuel += i_AdditionalFuel;
        }

        private void validateNoNegativeFuelAddition(float i_AdditionalFuel)
        {
            if (i_AdditionalFuel < k_ZeroFuel)
            {
                throw new ArgumentException("Cannot accept negative value");
            }
        }

        private void validateFuelType(string i_FuelType)
        {
            if (!m_FuelType.ToString().Equals(i_FuelType))
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append($"Invalid fuel type: The vehicle with license ID {m_Info.LicencePlate}");
                stringBuilder.Append($" accepts only {m_FuelType} fuel type.");
                throw new ArgumentException(stringBuilder.ToString());
            }
        }

        internal enum eFuelType : uint
        {
            Diesel = 1,
            Octan96,
            Octan95,
            Octan98,
        }

        internal static List<string> GetFueledVehicleFuelTypesOptions()
        {
            List<string> numOfDoorsOptions = new List<string>
            {
                eFuelType.Diesel.ToString(),
                eFuelType.Octan96.ToString(),
                eFuelType.Octan95.ToString(),
                eFuelType.Octan98.ToString()
            };

            return numOfDoorsOptions;
        }

        internal eFuelType ParseFuelType(string i_FuelType)
        {
            uint returnValue;

            if (!uint.TryParse(i_FuelType.ToString(), out returnValue))
            {
                throw new ArgumentException("Invalid vehicle fuel type input. Input must be a valid number.");
            }

            if (!Enum.IsDefined(typeof(eFuelType), returnValue))
            {
                throw new ArgumentException("Invalid fuel type");
            }

            return (eFuelType)returnValue;
        }

        protected internal void InitializeFuelInfo(eFuelType i_FuelType, float i_MaxFuelCapacity)
        {
            m_MaxFuelCapacity = i_MaxFuelCapacity;
            m_FuelType = i_FuelType;
        }

        internal override List<FieldClassInfo> GetFieldsList()
        {
            List<FieldClassInfo> fieldsInfo = base.GetFieldsList();

            fieldsInfo.AddRange(new List<FieldClassInfo>
            {
                new FieldClassInfo("RemainingFuel", "Please enter the amount of fuel (In Litres) you would like to fill: " , FieldClassInfo.eInputType.Float)
            });

            return fieldsInfo;
        }

        public sealed override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(base.ToString());
            stringBuilder.AppendLine("Fuel information:");
            stringBuilder.AppendLine($"Fuel type: {m_FuelType}");
            stringBuilder.AppendLine($"Fuel remaining: {m_RemainingFuel} Liters");
            stringBuilder.AppendLine($"Maximum fuel capacity: {m_MaxFuelCapacity} Liters");

            return stringBuilder.ToString();
        }
    }
}