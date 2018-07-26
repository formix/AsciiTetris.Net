using System;

namespace Formix.Games.Tetris.Display
{
    public class Sprite : IComparable<Sprite>
    {
        private static int spriteCount = 1;

        public string Name { get; private set; }
        public int ZIndex { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int Height { get => Content.GetUpperBound(0) + 1; }
        public int Width { get => Content.GetUpperBound(1) + 1; }

        public ColoredChar[,] Content { get; private set; }

        public Sprite(int height, int width, string name = null)
        {
            if (height <= 0 || width <= 0)
            {
                throw new ArgumentException("Height and width must be greater or equal to 1.");
            }

            Name = name ?? $"sprite{spriteCount++}";

            Content = new ColoredChar[height, width];

            ZIndex = 0;
            Row = 0;
            Col = 0;
        }

        public ColoredChar this[int row, int col]
        {
            get
            {
                return Content[row, col];
            }
        }

        public Sprite PrintH(int row, int col, char c, int count,
                ConsoleColor foreground = ConsoleColor.Gray,
                ConsoleColor background = ConsoleColor.Black)
        {
            if (row < 0 || row >= Height)
            {
                throw new ArgumentException(
                    "rowIndex must be 0 or greater and less than the sprite height.");
            }

            if (col < 0 || col >= Width)
            {
                throw new ArgumentException(
                    "startCol must be 0 or greater and less than the sprite width.");
            }

            if (count < 0 || count + col > Width)
            {
                throw new ArgumentException(
                    "count must be 0 or greater and startCol + count must be less than width.");
            }

            for (int currCol = col; currCol < col + count; currCol++)
            {
                Content[row, currCol] = new ColoredChar(c, foreground, background);
            }

            return this;
        }

        public Sprite PrintH(int row, int col, string str,
                ConsoleColor foreground = ConsoleColor.Gray,
                ConsoleColor background = ConsoleColor.Black)
        {
            int currCol = 0;
            foreach (var c in str)
            {
                if (col + currCol >= Width)
                {
                    return this;
                }
                PrintV(row, col + currCol, c, 1, foreground, background);
                currCol++;
            }

            return this;
        }

        public Sprite PrintV(int row, int col, char c, int count,
                ConsoleColor foreground = ConsoleColor.Gray,
                ConsoleColor background = ConsoleColor.Black)
        {
            if (col < 0 || col >= Width)
            {
                throw new ArgumentException(
                    "colIndex must be 0 or greater and less than the sprite width.");
            }

            if (row < 0 || row >= Height)
            {
                throw new ArgumentException(
                    "startRow must be 0 or greater and less than the sprite height.");
            }

            if (count < 0 || count + row > Height)
            {
                throw new ArgumentException(
                    "count must be 0 or greater and startRow + count have to be less than height.");
            }


            for (int r = row; r < row + count; r++)
            {
                Content[r, col] = new ColoredChar(c, foreground, background);
            }

            return this;
        }

        public Sprite PrintV(int row, int col, string str,
                ConsoleColor foreground = ConsoleColor.Gray,
                ConsoleColor background = ConsoleColor.Black)
        {
            int currRow = 0;
            foreach (var c in str)
            {
                if (row + currRow >= Height)
                {
                    return this;
                }
                PrintV(row + currRow, col, c, 1, foreground, background);
                currRow++;
            }

            return this;
        }


        public Sprite Clear()
        {
            for (int r = 0; r < Height; r++)
            {
                for (int c = 0; c < Width; c++)
                {
                    Content[r, c] = null;
                }
            }
            return this;
        }



        public ColoredChar GetChar(int row, int col)
        {
            return Content[row - Row, col - Col];
        }

        public bool Contains(int row, int col)
        {
            if (row >= Row && col >= Col)
            {
                if (row < Row + Height && col < Col + Width)
                {
                    return true;
                }
            }
            return false;
        }

        public int CompareTo(Sprite other)
        {
            if (ZIndex > other.ZIndex)
            {
                return 1;
            }
            else if (ZIndex < other.ZIndex)
            {
                return -1;
            }

            if (Row > other.Row)
            {
                return 1;
            }
            else if (Row < other.Row)
            {
                return -1;
            }

            if (Col > other.Col)
            {
                return 1;
            }
            else if (Col < other.Col)
            {
                return -1;
            }

            return 0;
        }
    }
}
