namespace MorskoyGoy
{
    using System;

    [Serializable]
    public class Position
    {

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

   
        public int X
        {
            get;
            private set;
        }


        public int Y
        {
            get;
            private set;
        }


        public bool Equals(Position other)
        {
            if (this.X != other.X || this.Y != other.Y)
            {
                return false;
            }

            return true;
        }
    }
}