using System.Text;

namespace Ex03.GarageLogic.DTO
{
    public sealed class VehicleInfoDTO
    {
        private string m_VehicleRecordInfo;
        private string m_VehicleSpecification;

        public string VehicleRecordInfo
        {
            get { return m_VehicleRecordInfo; }
            set { m_VehicleRecordInfo = value; }
        }

        public string VehicleSpecification
        {
            get { return m_VehicleSpecification; }
            set { m_VehicleSpecification = value; }
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();

            vehicleInfo.AppendLine(m_VehicleRecordInfo);
            vehicleInfo.AppendLine(m_VehicleSpecification);

            return vehicleInfo.ToString();
        }
    }
}