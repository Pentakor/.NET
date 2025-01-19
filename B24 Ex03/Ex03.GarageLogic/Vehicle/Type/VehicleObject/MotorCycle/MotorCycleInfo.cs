using Ex03.GarageLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.Vehicles.Types.Objects.MotorCycle
{
    internal class MotorCycleInfo : VehicleInfo
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        private const int k_ZeroVolume = 0;
        private const bool v_IsInfo = true;

        internal int EngineVolume
        {
            get { return m_EngineVolume; }
            set
            {
                if (value < k_ZeroVolume)
                {
                    throw new ArgumentException("Cannot accept negative value");
                }

                m_EngineVolume = value;
            }
        }

        internal enum eLicenseType : uint
        {
            A = 1,
            A1,
            AA,
            B1
        }

        internal void SetLicenseType(string i_LicenseTypeInput)
        {
            m_LicenseType = parseLicenseType(i_LicenseTypeInput);
        }

        private eLicenseType parseLicenseType(string i_LicenseTypeInput)
        {
            uint  returnValue;

            if (!uint.TryParse(i_LicenseTypeInput, out returnValue))
            {
                throw new ArgumentException("Invalid license type input. Input must be a valid number.");
            }

            if (!Enum.IsDefined(typeof(eLicenseType), returnValue))
            {
                throw new ArgumentException("Invalid license type");
            }
       
            return (eLicenseType)returnValue;
        }

        private List<string> getMotorCycleLicenseOptions()
        {
            List<string> numOfDoorsOptions = new List<string>
            {
                eLicenseType.A.ToString(),
                eLicenseType.A1.ToString(),
                eLicenseType.AA.ToString(),
                eLicenseType.B1.ToString()
            };

            return numOfDoorsOptions;
        }

        internal override List<FieldClassInfo> GetFieldsInfos()
        {
            List<FieldClassInfo> fieldsInfo = base.GetFieldsInfos();

            fieldsInfo.AddRange(new List<FieldClassInfo>
            {
                new FieldClassInfo("SetLicenseType", "Please enter motorcycle's license: ", FieldClassInfo.eInputType.String, v_IsInfo, getMotorCycleLicenseOptions()),
                new FieldClassInfo("EngineVolume", "Please enter motorcycle's engine volume: ", FieldClassInfo.eInputType.Int, v_IsInfo)
            });

            return fieldsInfo;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(base.ToString());
            stringBuilder.AppendLine("Motorcycle Details:");
            stringBuilder.AppendLine($"License type: {m_LicenseType}");
            stringBuilder.AppendLine($"Motorcycle's engine volume: {m_EngineVolume}cc");

            return stringBuilder.ToString();
        }
    }
}