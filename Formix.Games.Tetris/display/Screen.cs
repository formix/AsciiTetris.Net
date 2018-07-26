using System;
using System.Collections.Generic;
using System.Text;

namespace Formix.Games.Tetris.Display
{
    public class Screen
    {
        private ColoredChar[,] _currentFrame;

        public int Height { get => _currentFrame.GetUpperBound(0) + 1;  }
        public int Width { get => _currentFrame.GetUpperBound(1) + 1; }
        public SortedSet<Canvas> Canvases { get; private set; }

        public Screen()
        {
            Canvases = new SortedSet<Canvas>();
            Initialize();
        }

        public void Refresh()
        {
            Initialize();
            foreach (var canvas in Canvases)
            {
                canvas.Refresh();
            }
            Project();
        }

        public Screen Add(Frame frame)
        {
            Canvases.Add(frame);
            return this;
        }

        public bool Project(Func<Canvas, IEnumerable<Collision>, bool> handleCollisions = null)
        {
            lock (_currentFrame)
            {
                bool continueProjection = true;
                ICollection<Projection> projections = new LinkedList<Projection>();
                for (int row = 0; row < _currentFrame.GetUpperBound(0); row++)
                {
                    for (int col = 0; col < _currentFrame.GetUpperBound(1); col++)
                    {
                        ColoredChar c = null;
                        if (handleCollisions == null)
                        {
                            c = GetChar(row, col);
                        }
                        else
                        {
                            c = GetChar(row, col,
                                (canvas, collisions) => continueProjection = handleCollisions(canvas, collisions));
                        }

                        if (!continueProjection)
                        {
                            return false;
                        }

                        projections.Add(new Projection(row, col, c));
                    }
                }

                foreach (var projection in projections)
                {
                    Project(projection);
                }

                return true;
            }
        }

        private void Project(Projection projection)
        {
            var row = projection.Row;
            var col = projection.Col;
            var c = projection.Char;

            if (!c.Equals(_currentFrame[row, col]))
            {
                Console.CursorTop = row;
                Console.CursorLeft = col;
                Console.BackgroundColor = c.Background;
                Console.ForegroundColor = c.Foreground;
                Console.Write(c.Char);
                if (c.IsNull)
                {
                    _currentFrame[row, col] = null;
                }
                else
                {
                    _currentFrame[row, col] = c;
                }
            }
        }


        private void Initialize()
        {
            Console.Clear();
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            _currentFrame = new ColoredChar[Console.WindowHeight, Console.WindowWidth];
            Console.SetBufferSize(
                Console.WindowLeft + Console.WindowWidth,
                Console.WindowTop + Console.WindowHeight);
        }


        private ColoredChar GetChar(int row, int col, Action<Canvas, IEnumerable<Collision>> hit = null)
        {
            if (row < 0 || row >= Height)
            {
                throw new ArgumentException($"row must be between 0 and {Height - 1}");
            }

            if (col < 0 || col >= Width)
            {
                throw new ArgumentException($"col must be between 0 and {Width - 1}");
            }

            ColoredChar currChar = new ColoredChar();
            foreach (var canvas in Canvases)
            {
                if (canvas.Contains(row, col))
                {
                    ColoredChar c = null;

                    if (hit == null)
                    {
                        c = canvas.GetChar(row, col);
                    }
                    else
                    {
                        c = canvas.GetChar(row, col, (collisions) => hit(canvas, collisions));
                    }
                    currChar.Import(c, currChar.Background);
                }
            }

            return currChar;
        }
    }
}
