using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.GarageManager
{
    public class VehicleRecord
    {
        private Owner m_VehicleOwner;
        private eVehicleStatus m_VehicleStatus;

        public VehicleRecord(Owner i_Owner, eVehicleStatus i_VehicleStatus = eVehicleStatus.InRepair)
        {
            m_VehicleOwner = i_Owner;
            m_VehicleStatus = i_VehicleStatus;
        }

        public Owner VehicleOwner
        {
            get { return m_VehicleOwner; }
        }

        public string VehicleStatus
        {
            get { return m_VehicleStatus.ToString(); }
            set
            {
                if (!Enum.TryParse(value, true, out eVehicleStatus returnValue))
                {
                    throw new FormatException("There is no such status.");
                }

                m_VehicleStatus = returnValue;
            }
        }

        public enum eVehicleStatus : uint
        {
            InRepair = 1,
            Repaired,
            Paid,
        }

        internal static List<string> GetVehicleStatusesOptions()
        {
            List<string> numOfDoorsOptions = new List<string>
            {
                eVehicleStatus.InRepair.ToString(),
                eVehicleStatus.Repaired.ToString(),
                eVehicleStatus.Paid.ToString(),
            };

            return numOfDoorsOptions;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(m_VehicleOwner.ToString());
            stringBuilder.AppendLine($"Vehicle status: {m_VehicleStatus}");

            return stringBuilder.ToString();
        }
    }
}
