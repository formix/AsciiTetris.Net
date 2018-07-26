namespace Formix.Games.Tetris.Display
{
    public class Projection
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public ColoredChar Char { get; set; }

        public Projection(int row, int col, ColoredChar c)
        {
            Row = row;
            Col = col;
            Char = c;
        }
    }
}
