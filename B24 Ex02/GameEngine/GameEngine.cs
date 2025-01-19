using System;
using System.Collections.Generic;
using MemoryGame.Board;
using MemoryGame.DTO;

namespace MemoryGame.Engine
{
    public class MemoryGameEngine<T>
    {
        private GameBoard<T> m_GameBoard;
        private readonly GameBotUtills r_GameBotUtillizer;
        private List<GameInfoPlayer> m_ListOfPlayers;
        private readonly T[] r_Symbols;
        private readonly BoardJudge r_BoardJudge;
        private static readonly Random sr_Random = new Random();
        private const int k_ScoreUnit = 1;
        private const int k_FirstPlaceInList = 0;
        private const int k_GroupOfSimilarSqueresAmount = 2;

        public GameBoard<T> Board
        {
            get { return m_GameBoard; }
        }

        public GameBotUtills GameBotUtillizer
        {
            get { return r_GameBotUtillizer; }

        }

        public BoardJudge GameBoardJudge
        {
            get { return r_BoardJudge; }
        }

        public MemoryGameEngine(GameSettings<T> i_GameSettings )
        {
            m_ListOfPlayers = new List<GameInfoPlayer>();
            m_GameBoard = new GameBoard<T>(i_GameSettings.BoardRows, i_GameSettings.BoardColumns);
            initializeBoard(i_GameSettings.Symbols);
            initializePlayersList(i_GameSettings.ListOfPlayersData);
            r_GameBotUtillizer = new GameBotUtills(m_GameBoard.GetListOfCoordinates());
            r_Symbols = i_GameSettings.Symbols;
            r_BoardJudge = new BoardJudge(this);
        }  
        
        private void initializePlayersList(List<PlayerData> i_ListOfPlayersData)
        {
            foreach(PlayerData playerData in i_ListOfPlayersData)
            {
                m_ListOfPlayers.Add(new GameInfoPlayer(playerData));
            }
        }

        public static bool IsEvenBoardVolume(uint i_RowToCheck, uint i_ColumnToCheck)
        {
            return (i_RowToCheck * i_ColumnToCheck) % 2 == 0;
        }

        public void HidePairOfSquares(SquareCoordinate i_FirstCoordinate, SquareCoordinate i_SecondCoordinate)
        {
            m_GameBoard.HideSquare(i_FirstCoordinate);
            m_GameBoard.HideSquare(i_SecondCoordinate);
        }

        private void initializeBoard(T[] i_Symbols)
        {
            T[] shuffledSymbols = generateSymbols(i_Symbols);
            T newSymbol;
            SquareCoordinate coordinate = new SquareCoordinate();

            for (uint row = 0; row < m_GameBoard.NumberOfRows; row++)
            {
                coordinate.Row = row;
                for (uint col = 0; col < m_GameBoard.NumberOfColumns; col++)
                {
                    coordinate.Column = col;
                    newSymbol = shuffledSymbols[row * m_GameBoard.NumberOfColumns + col];
                    m_GameBoard.UpdateSquareSymbol(coordinate, newSymbol);
                }
            }
        }

        private T[] generateSymbols(T[] i_Symbols)
        {
            uint numberOfSquares = m_GameBoard.BoardVolume;
            T[] symbols = new T[numberOfSquares];
            int iterator = 0;
            T symbol = i_Symbols[iterator];

            for(int i = 0; i < numberOfSquares; i += k_GroupOfSimilarSqueresAmount)
            {
                symbols[i] = symbol;
                symbols[i + 1] = symbol;
                symbol = i_Symbols[++iterator];
            }

            return shuffle(symbols);
        }

        private T[] shuffle(T[] io_Symbols)
        {
            int destShuffleIndex = io_Symbols.Length;
            T tempValue;
            int srcShuffleIndex;

            while (destShuffleIndex > 0)
            {
                destShuffleIndex--;
                srcShuffleIndex = sr_Random.Next(destShuffleIndex + 1);
                tempValue = io_Symbols[destShuffleIndex];
                io_Symbols[destShuffleIndex] = io_Symbols[srcShuffleIndex];
                io_Symbols[srcShuffleIndex] = tempValue;
            }

            return io_Symbols;
        }

        public void reInitializeBoardSettings(uint i_NewNumberOfRows, uint i_NewNumberOfColumns)
        {
            m_GameBoard = new GameBoard<T>(i_NewNumberOfRows, i_NewNumberOfColumns);
           
            foreach(GameInfoPlayer player in m_ListOfPlayers)
            {
                player.Score = 0;
            }

            r_BoardJudge.ResetGameRoundPlayer();
            initializeBoard(r_Symbols);
        }

        public class GameBotUtills
        {
            private List<SquareCoordinate> m_ListOfHiddenSquares;
            
            internal GameBotUtills(List<SquareCoordinate> i_ListOfCoordinates)
            {
                m_ListOfHiddenSquares = i_ListOfCoordinates;
            }

            public void RemoveSquareFromHiddenList(SquareCoordinate i_Coordinate)
            {
                if (m_ListOfHiddenSquares.Count > 0)
                {
                    SquareCoordinate squareToRemove = m_ListOfHiddenSquares.Find(coord => coord.Equals(i_Coordinate));
                    if (squareToRemove != null)
                    {
                        m_ListOfHiddenSquares.Remove(squareToRemove);
                    }
                }
            }

            public SquareCoordinate makeRandomMove(SquareCoordinate i_RestrictedSquare = null) 
            {
                SquareCoordinate coordinate = m_ListOfHiddenSquares[sr_Random.Next(m_ListOfHiddenSquares.Count)];

                if(i_RestrictedSquare != null)
                {
                    while(coordinate.Equals(i_RestrictedSquare))
                    {
                        coordinate = m_ListOfHiddenSquares[sr_Random.Next(m_ListOfHiddenSquares.Count)];
                    }
                }

                return coordinate;
            }
        }

        public class BoardJudge
        {
            private readonly MemoryGameEngine<T> r_GameEngine;
            private GameInfoPlayer m_CurrentPlayer;
            private List<GameInfoPlayer> m_ListOfWinners;

            internal BoardJudge(MemoryGameEngine<T> i_GameEngine)
            {
                r_GameEngine = i_GameEngine;
                m_CurrentPlayer = i_GameEngine.m_ListOfPlayers[0];
                m_ListOfWinners = new List<GameInfoPlayer>();
            }

            public GameInfoPlayer CurrentPlayer
            {
                get { return m_CurrentPlayer; }
            }    

            public bool IsEndOfGame()
            {      
                return r_GameEngine.Board.NumberOfRevealedSquares == r_GameEngine.Board.BoardVolume;
            }

            private void calculateFinalWinnersList()
            {
                GameInfoPlayer highestScorePlayerInfo = r_GameEngine.m_ListOfPlayers[k_FirstPlaceInList];
                
                foreach (GameInfoPlayer player in r_GameEngine.m_ListOfPlayers)
                {
                    if(player.Score >= highestScorePlayerInfo.Score)
                    {
                        highestScorePlayerInfo = player;
                    }
                }

                m_ListOfWinners.Add(highestScorePlayerInfo);
                foreach(GameInfoPlayer player in r_GameEngine.m_ListOfPlayers)
                {
                    if(player.Score == highestScorePlayerInfo.Score)
                    {
                        m_ListOfWinners.Add(player);
                    }
                }
            }

            public bool IsPairOfSquaresValuesAreEqual(SquareCoordinate i_FirstCoordinate, SquareCoordinate i_SecondCoordinate)
            {
                return r_GameEngine.m_GameBoard.GetSquareValue(i_FirstCoordinate) == r_GameEngine.m_GameBoard.GetSquareValue(i_SecondCoordinate);
            }

            public void UpdateCurrentPlayerScore()
            {
                m_CurrentPlayer.Score += k_ScoreUnit;
            }

            public void AdvanceTurnToNextPlayer()
            {
                int currentPlayerIndex = r_GameEngine.m_ListOfPlayers.IndexOf(m_CurrentPlayer);
                int nextPlayerIndex = currentPlayerIndex + 1;
                int numberOfPlayersInGame = r_GameEngine.m_ListOfPlayers.Count; 

                m_CurrentPlayer = r_GameEngine.m_ListOfPlayers[nextPlayerIndex % numberOfPlayersInGame];
            }

            internal void ResetGameRoundPlayer()
            {
                m_CurrentPlayer = r_GameEngine.m_ListOfPlayers[0];
            }

            public GameStatistics GetGameStatistics()
            {
                List<GameInfoPlayer> listOfPlayer = r_GameEngine.m_ListOfPlayers;
                bool isDraw;
                GameInfoPlayer winnerPlayer = null;

                calculateFinalWinnersList();
                isDraw = m_ListOfWinners.Count == 1;
                if (!isDraw)
                {
                    winnerPlayer = m_ListOfWinners[k_FirstPlaceInList];
                }

                return new GameStatistics(listOfPlayer, isDraw, winnerPlayer);
            }
        }
    }
}
