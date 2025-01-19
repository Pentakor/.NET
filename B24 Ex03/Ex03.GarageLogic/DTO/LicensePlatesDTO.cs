using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.DTO
{
    public sealed class LicensePlatesDTO
    {
        private Dictionary<string, List<string>> m_LicensePlatesDict;

        public Dictionary<string, List<string>> LicensePlatesDict
        {
            get { return m_LicensePlatesDict; }
            set { m_LicensePlatesDict = value; }
        }

        public LicensePlatesDTO()
        {
            m_LicensePlatesDict = new Dictionary<string, List<string>>();
        }

        private string printListOfLicensePlates(List<string> i_LicensePlatesList)
        {
            return string.Join(", ", i_LicensePlatesList);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, List<string>> kvp in m_LicensePlatesDict)
            {
                stringBuilder.Append($"{kvp.Key}: ");
                stringBuilder.AppendLine(printListOfLicensePlates(kvp.Value));
            }

            return stringBuilder.ToString();
        }
    }
}