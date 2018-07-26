using System;
using System.Collections.Generic;
using System.Linq;

namespace Formix.Games.Tetris.Display
{
    public class Canvas : IComparable<Canvas>
    {
        private static int _nextId = 1;

        public ConsoleColor Background { get; set; }
        public int ZIndex { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public SortedSet<Sprite> Sprites { get; private set; }

        public Canvas(int height, int width,
                ConsoleColor background = ConsoleColor.Black)
        {
            if (height <= 0 || width <= 0)
            {
                throw new ArgumentException("Height and width must be greater or equal to 1.");
            }

            Sprites = new SortedSet<Sprite>();
            Row = 0;
            Col = 0;
            Height = height;
            Width = width;
            ZIndex = 0;
            Background = background;
        }


        public int CompareTo(Canvas other)
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

        public void Refresh()
        {
            lock (this)
            {
                Sprites = new SortedSet<Sprite>(Sprites.ToArray());
            }
        }

        public bool Contains(int row, int col)
        {
            if (row >= Row && col >= Col)
            {
                if (row < Row + Height && col < Col + Width)
                {
                    return GetChar(row, col) != null;
                }
            }
            return false;
        }

        public ColoredChar GetChar(int row, int col, Action<IEnumerable<Collision>> hit = null)
        {
            if (row - Row < 0 || row - Row >= Height)
            {
                throw new ArgumentException($"row must be between 0 and {Height - 1}");
            }

            if (col - Col < 0 || col - Col >= Width)
            {
                throw new ArgumentException($"col must be between 0 and {Width - 1}");
            }

            ColoredChar currChar = new ColoredChar
            {
                Background = Background
            };

            lock (Sprites)
            {
                Dictionary<int, Collision> collisionTable = null;
                if (hit != null)
                {
                    collisionTable = new Dictionary<int, Collision>();
                }

                foreach (var sprite in Sprites)
                {
                    if (sprite.Contains(row - Row, col - Col))
                    {
                        var spriteChar = sprite.GetChar(row - Row, col - Col);
                        currChar.Import(spriteChar, currChar.Background);

                        if (collisionTable != null && spriteChar != null && !spriteChar.IsNull)
                        {
                            // Builds the collision table
                            if (!collisionTable.ContainsKey(sprite.ZIndex))
                            {
                                collisionTable.Add(ZIndex, new Collision());
                            }
                            collisionTable[sprite.ZIndex].Add(sprite);
                        }
                    }
                }

                if (hit != null)
                {
                    var collisions =
                        from c in collisionTable.Values
                        where c.Count > 1
                        select c;

                    if (collisions.Count() > 0)
                    {
                        hit(collisions);
                    }
                }
            }

            return currChar;
        }

    }
}
