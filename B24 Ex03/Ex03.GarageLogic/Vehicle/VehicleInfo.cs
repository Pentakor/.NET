using Ex03.GarageLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.Vehicles
{
    internal class VehicleInfo
    {
        protected string m_ModelName;
        protected string m_LicensePlate;
        protected float m_RemaningEnergyPercentage;
        private const int k_OneHundredPercent = 100;
        private const bool v_IsInfo = true;

        internal string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        internal string LicencePlate
        {
            get { return m_LicensePlate; }
            set { m_LicensePlate = value; }
        }

        protected internal void AdjustPercentage(float i_RemaningEnergy, float i_MaxEnergyCapacity)
        {
            m_RemaningEnergyPercentage = (i_RemaningEnergy / i_MaxEnergyCapacity) * k_OneHundredPercent;
        }

        internal virtual List<FieldClassInfo> GetFieldsInfos()
        {
            return new List<FieldClassInfo>
            {
                new FieldClassInfo("ModelName", "Please enter vehicle's model: ", FieldClassInfo.eInputType.String, v_IsInfo)
            };
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Vehicle Details:");
            stringBuilder.AppendLine($"Model Name: {ModelName}");
            stringBuilder.AppendLine($"Licence Plate: {LicencePlate}");
            stringBuilder.AppendLine($"Remaining Energy Percentage: {m_RemaningEnergyPercentage}{Environment.NewLine}%");

            return stringBuilder.ToString();
        }
    }
}