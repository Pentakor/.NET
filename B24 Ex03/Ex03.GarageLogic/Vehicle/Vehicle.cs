using Ex03.GarageLogic.DTO;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.Vehicles
{
    internal abstract class Vehicle
    {
        protected VehicleInfo m_Info;
        protected List<Wheel> m_ListOfWheels;

        internal VehicleInfo Info
        {
            get { return m_Info; }
            set { m_Info = value; }
        }

        internal List<Wheel> ListOfWheels
        {
            get { return m_ListOfWheels; }
        }

        protected internal void InitializeWheels(int i_NumOfWheels, float i_MaxAirPressure)
        {
            m_ListOfWheels = new List<Wheel>(i_NumOfWheels);
            for (int i = 0; i < m_ListOfWheels.Capacity; i++)
            {
                m_ListOfWheels.Add(new Wheel(i_MaxAirPressure));
            }
        }

        internal void InflateTiresToMaximumAirPressure()
        {
            foreach (Wheel wheel in m_ListOfWheels)
            {
                wheel.InflateTireToMax();
            }
        }

        internal virtual List<FieldClassInfo> GetFieldsList()
        {
            List<FieldClassInfo> fieldsInfo = new List<FieldClassInfo>();

            fieldsInfo.AddRange(m_Info.GetFieldsInfos());
            fieldsInfo.Add(new FieldClassInfo("ListOfWheels", null));
            fieldsInfo.Add(new FieldClassInfo("SetManufacturedNameForAllWheels", "Please enter wheels manufacturor name: "));
            fieldsInfo.Add(new FieldClassInfo("SetCurrentPressureForAllWheels", "Please enter wheels current air pressure: ", FieldClassInfo.eInputType.Float));

            return fieldsInfo;
        }

        internal void SetManufacturedNameForAllWheels(string i_ManufacturedName)
        {
            foreach (Wheel wheel in m_ListOfWheels)
            {
                wheel.ManufacturerName = i_ManufacturedName;
            }
        }

        internal void SetCurrentPressureForAllWheels(float i_CurrentPressure)
        {
            foreach (Wheel wheel in m_ListOfWheels)
            {
                wheel.InflateTire(i_CurrentPressure);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 1;

            stringBuilder.AppendLine(m_Info.ToString());
            stringBuilder.AppendLine("Wheels Information:");
            foreach (Wheel wheel in m_ListOfWheels)
            {
                stringBuilder.AppendLine($"Wheel #{i++}");
                stringBuilder.AppendLine(wheel.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}