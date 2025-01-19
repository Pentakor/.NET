using Ex03.GarageLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.Vehicles.Types.Objects.Truck
{
    internal class TruckInfo : VehicleInfo
    {
        private bool m_DangerousMaterial;
        private float m_CarrierCapacity;
        private const float k_ZeroCapaciy = 0f;
        private const bool v_IsInfo = true;

        internal bool DangerousMaterial
        {
            get { return m_DangerousMaterial; }
            set { m_DangerousMaterial = value; }
        }

        internal float CarrierCapacity
        {
            get { return m_CarrierCapacity; }
            set
            {
                if (value < k_ZeroCapaciy)
                {
                    throw new ArgumentException("Cannot accept negative value");
                }

                m_CarrierCapacity = value;
            }
        }

        internal void SetDangerousMaterials(int i_Choice)
        {
            if(i_Choice != 1 && i_Choice != 2)
            {
                throw new FormatException("You should choose between 1 or 2.");
            }
            DangerousMaterial = Convert.ToBoolean(i_Choice - 1);
        }

        internal override List<FieldClassInfo> GetFieldsInfos()
        {
            List<FieldClassInfo> fieldsInfo = base.GetFieldsInfos();

            fieldsInfo.AddRange(new List<FieldClassInfo>
            {
                new FieldClassInfo("SetDangerousMaterials", "Please enter whether your truck carries dangerous materials: ", FieldClassInfo.eInputType.Int, v_IsInfo, new List<string>{ "No", "Yes"}),
                new FieldClassInfo("CarrierCapacity", "Please enter truck's carrier capacity: ", FieldClassInfo.eInputType.Float, v_IsInfo)
            });

            return fieldsInfo;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(base.ToString());
            stringBuilder.AppendLine("Truck Details:");
            stringBuilder.Append("Carrying dangerous materials?: ");
            stringBuilder.AppendLine(m_DangerousMaterial ? "Yes" : "No");
            stringBuilder.AppendLine($"Carrier capacity: {m_CarrierCapacity}");

            return stringBuilder.ToString();
        }
    }
}