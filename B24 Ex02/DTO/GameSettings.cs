using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryGame.DTO
{
    public class GameSettings<T>
    {
        private List<PlayerData> r_ListOfPlayersData;
        private HashSet<string> m_UsedBotNames = new HashSet<string>();
        private uint m_BoardRows;
        private uint m_BoardColumns;
        private T[] m_Symbols;
        private const bool v_Bot = true;
        private const uint k_BotNameNumberLength = 6;

        public GameSettings(T[] i_Symbols)
        {
            r_ListOfPlayersData = new List<PlayerData>();
            Symbols = i_Symbols;
        }

        public void AddPlayerToListOfPlayers(string i_PlayerName)
        {
            r_ListOfPlayersData.Add(new PlayerData(i_PlayerName, !v_Bot));
        }

        public void AddBotToListOfPlayers()
        {
            string botName = generateUniqueRandomBotName();

            r_ListOfPlayersData.Add(new PlayerData(botName, v_Bot));
        }

        private string generateUniqueRandomBotName()
        {
            string botName;

            do
            {
                botName = generateRandomBotName();
            }while(m_UsedBotNames.Contains(botName));

            m_UsedBotNames.Add(botName);

            return botName;
        }

        private string generateRandomBotName()
        {
            StringBuilder stringBuilder = new StringBuilder("bot_");
            Random random = new Random();

            for(int i = 0; i < k_BotNameNumberLength; i++)
            {
                stringBuilder.Append(random.Next(10)); 
            }

            return stringBuilder.ToString();
        }

        public string GetFirstPlayerName()
        {
            return r_ListOfPlayersData[0].Name;
        }

        internal List<PlayerData> ListOfPlayersData
        {
            get { return r_ListOfPlayersData; }
        }

        public uint BoardRows
        {
            get { return m_BoardRows; }
            set { m_BoardRows = value; }
        }

        public uint BoardColumns 
        {
            get { return m_BoardColumns; }
            set { m_BoardColumns = value; }
        }

        internal T[] Symbols
        {
            get { return m_Symbols; }
            set { m_Symbols = value; }
        }
    }
}
