using System.Collections.Generic;

namespace Ex03.GarageLogic.DTO
{
    public class FieldClassInfo
    {
        private string m_FieldName;
        private string m_PromptMessage;
        private eInputType m_InputType;
        private List<string> m_Options;
        private bool m_IsInfo;

        public enum eInputType
        {
            String,
            Float,
            Char,
            Int
        }

        public bool IsInfo
        {
            get { return m_IsInfo; }
            set { m_IsInfo = value; }
        }

        public string FieldName
        {
            get { return m_FieldName; }
            set { m_FieldName = value; }
        }

        public eInputType InputType
        {
            get { return m_InputType; }
            set { m_InputType = value; }
        }

        public string PromptMessage
        {
            get { return m_PromptMessage; }
            set { m_PromptMessage = value; }
        }

        public List<string> Options
        {
            get { return m_Options; }
            set { m_Options = value; }
        }

        public FieldClassInfo(string i_FieldName, string i_PromptMessage, eInputType i_InputType = eInputType.String, bool i_IsInfo = false, List<string> i_Options = null)
        {
            m_FieldName = i_FieldName;
            m_PromptMessage = i_PromptMessage;
            m_InputType = i_InputType;
            m_Options = i_Options ?? new List<string>();
            m_IsInfo = i_IsInfo;
        }

        public FieldClassInfo(string i_FieldName, string i_PromptMessage, bool i_IsInfo = false, List<string> i_Options = null)
        {
            m_FieldName = i_FieldName;
            m_PromptMessage = i_PromptMessage;
            m_Options = i_Options ?? new List<string>();
            m_IsInfo = i_IsInfo;
        }

        public FieldClassInfo(string i_FieldName, string i_PromptMessage, List<string> i_Options = null)
        {
            m_FieldName = i_FieldName;
            m_PromptMessage = i_PromptMessage;
            m_Options = i_Options ?? new List<string>();
        }

        public FieldClassInfo(string i_FieldName, string i_PromptMessage)
        {
            m_FieldName = i_FieldName;
            m_PromptMessage = i_PromptMessage;
        }
    }
}