namespace MemoryGame.DTO
{
    public class SquareCoordinate
    {
        private uint m_Row;
        private uint m_Column;

        public SquareCoordinate()
        {
            m_Row = 0;
            m_Column = 0;
        }

        public SquareCoordinate(uint i_Row, uint i_Column)
        {
            m_Row = i_Row;
            m_Column = i_Column;
        }

        public uint Column
        {
            get { return m_Column; }
            set { m_Column = value; }
        }

        public uint Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public override bool Equals(object obj)
        {
            bool returnValue = false;

            if (obj is SquareCoordinate)
            {
                SquareCoordinate other = obj as SquareCoordinate;
                returnValue = this.Row == other.Row && this.Column == other.Column;
            }

            return returnValue;
        }

        public override int GetHashCode() { return 0; }
    }
}