namespace MemoryGame.DTO
{
    public class GameInfoPlayer
    {
        private readonly string r_Name;
        private uint m_Score;
        private readonly bool r_Bot;

        internal GameInfoPlayer(PlayerData i_PlayerData, uint i_Score = 0)
        {
            r_Name = i_PlayerData.Name;
            m_Score = i_Score;
            r_Bot = i_PlayerData.Bot;
        }

        public string Name
        {
            get { return r_Name; }
        }

        public uint Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public bool Bot
        {
            get { return r_Bot; }
        }
    }
}
