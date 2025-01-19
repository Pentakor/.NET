using System.Collections.Generic;
using MemoryGame.DTO;

namespace MemoryGame.Board
{
    public class GameBoard<T>
    {
        private const bool v_Revealed = true;
        private readonly Square<T>[,] r_Board;
        private uint m_NumberOfRevealedSquares;
        private readonly uint r_NumberOfRows;
        private readonly uint r_NumberOfColumns;
        private readonly uint r_BoardVolume;

        public uint NumberOfRows
        {
            get { return r_NumberOfRows; }    
        }

        public uint NumberOfColumns
        {
            get { return r_NumberOfColumns; }
        }

        internal uint BoardVolume
        {
            get { return r_BoardVolume; }
        }

        internal uint NumberOfRevealedSquares
        {
            get { return m_NumberOfRevealedSquares; }
        }

        internal GameBoard(uint i_NumberOfRows, uint i_NumberOfColumns)
        {
            r_NumberOfColumns = i_NumberOfColumns;
            r_NumberOfRows = i_NumberOfRows;
            r_BoardVolume = r_NumberOfColumns * r_NumberOfRows;
            r_Board = new Square<T>[NumberOfRows, NumberOfColumns];
            m_NumberOfRevealedSquares = 0;
        }

        internal List<SquareCoordinate> GetListOfCoordinates()
        {
            List<SquareCoordinate> listOfCoordinates = new List<SquareCoordinate>();

            for(uint row = 0; row < NumberOfRows; row++)
            {
                for(uint column = 0; column < NumberOfColumns; column++)
                {
                    listOfCoordinates.Add(new SquareCoordinate(row, column));
                }
            }

            return listOfCoordinates;
        }

        internal void UpdateSquareSymbol(SquareCoordinate i_Coordinate, T i_NewSymbol)
        {
            r_Board[i_Coordinate.Row, i_Coordinate.Column].Symbol = i_NewSymbol;
        }

        private bool isSquareRevealed(SquareCoordinate i_Coordinate)
        {
            return r_Board[i_Coordinate.Row, i_Coordinate.Column].Revealed;
        }

        public bool RevealSquare(SquareCoordinate i_Coordinate)
        {
            bool revealed = !v_Revealed;

            if(!isSquareRevealed(i_Coordinate))
            {
                revealed = (r_Board[i_Coordinate.Row, i_Coordinate.Column].Revealed = v_Revealed);
                m_NumberOfRevealedSquares++;
            }

            return revealed;
        }

        internal bool HideSquare(SquareCoordinate i_Coordinate)
        {
            bool hidden = !v_Revealed;

            if(isSquareRevealed(i_Coordinate))
            {
                hidden = (r_Board[i_Coordinate.Row, i_Coordinate.Column].Revealed = !v_Revealed);
                m_NumberOfRevealedSquares--;
            }

            return hidden;
        }

        public string GetSquareValue(SquareCoordinate i_Coordinate)
        {
            return isSquareRevealed(i_Coordinate) ? getSquareRevealedValue(i_Coordinate).ToString() : " ".ToString();
        }

        private T getSquareRevealedValue(SquareCoordinate i_Coordinate)
        {
            return r_Board[i_Coordinate.Row, i_Coordinate.Column].Symbol;
        }

        private struct Square<U>
        {
            private U m_Symbol;
            private bool m_Revealed;
      
            public U Symbol
            {
                get { return m_Symbol; }
                set { m_Symbol = value; }
            }

            public bool Revealed
            {
                get { return m_Revealed; }
                set { m_Revealed = value; }
            }
        }
    }
}
