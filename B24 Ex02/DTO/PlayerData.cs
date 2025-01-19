namespace MemoryGame.DTO
{
    internal struct PlayerData
    {
        private readonly string r_Name;
        private readonly bool r_Bot;

        internal bool Bot
        {
            get { return r_Bot; }
        }

        internal string Name
        {
            get { return r_Name; }
        }

        internal PlayerData(string i_Name, bool i_Bot)
        {
            r_Name = i_Name;
            r_Bot = i_Bot;
        }
    }
}
