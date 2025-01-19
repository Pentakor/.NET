using System.Collections.Generic;

namespace MemoryGame.DTO
{
    public class GameStatistics
    {
        private readonly List<GameInfoPlayer> r_ListOfPlayers;
        private readonly bool r_Draw;
        private readonly GameInfoPlayer r_WinnerPlayer;

        public List<GameInfoPlayer> ListOfPlayers
        {
            get { return r_ListOfPlayers; }
        }

        public GameInfoPlayer WinnerPlayer
        {
            get { return r_WinnerPlayer; }
        }

        public bool Draw
        {
            get { return r_Draw; }
        }

        internal GameStatistics(List<GameInfoPlayer> i_ListOfPlayers, bool i_Draw, GameInfoPlayer i_WinnerPlayer = null)
        {
            r_ListOfPlayers = i_ListOfPlayers;
            r_Draw = i_Draw;
            r_WinnerPlayer = i_WinnerPlayer;
        }
    }
}
