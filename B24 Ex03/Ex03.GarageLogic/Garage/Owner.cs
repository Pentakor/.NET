using Ex03.GarageLogic.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ex03.GarageLogic.GarageManager
{
    public class Owner
    {
        private string m_Name;
        private string m_PhoneNumber;

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (isValidName(value))
                {
                    m_Name = value;
                }
                else
                {
                    throw new ArgumentException("Name can only contain letters and special characters.");
                }
            }
        }

        public string PhoneNumber
        {
            get { return m_PhoneNumber; }
            set
            {
                if (isValidPhoneNumber(value))
                {
                    m_PhoneNumber = value;
                }
                else
                {
                    throw new ArgumentException("Phone number can only contain numbers and the + prefix character.");
                }
            }
        }

        public Owner(string i_Name, string i_PhoneNumber)
        {
            Name = i_Name;
            PhoneNumber = i_PhoneNumber;
        }

        public Owner()
        {
            m_Name = string.Empty;
            m_PhoneNumber = string.Empty;
        }

        public List<FieldClassInfo> GetFieldsInfo()
        {
            return new List<FieldClassInfo>
            {
                new FieldClassInfo("Name", "Please enter vehicle's owner name: "),
                new FieldClassInfo("PhoneNumber", "Please enter vehicle's owner phone number: ")
            };
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Owner name: {m_Name}");
            stringBuilder.AppendLine($"Owner phone number: {m_PhoneNumber}");

            return stringBuilder.ToString();
        }

        private bool isValidName(string i_Name)
        {
            return Regex.IsMatch(i_Name, @"^[a-zA-Z\s\p{P}\p{S}]+$");
        }

        private bool isValidPhoneNumber(string i_PhoneNumber)
        {
            return Regex.IsMatch(i_PhoneNumber, @"^\+?[0-9]+$");
        }
    }
}
