using System;

namespace Formix.Games.Tetris.Display
{
    public class ColoredChar
    {
        public ConsoleColor Foreground { get; set; }
        public ConsoleColor Background { get; set; }
        public char Char { get; set; }

        public ColoredChar(char c = ' ',
                ConsoleColor foreground = ConsoleColor.Gray,
                ConsoleColor background = ConsoleColor.Black)
        {
            Char = c;
            Background = background;
            Foreground = foreground;
        }

        public bool IsNull
        {
            get
            {
                return Char == ' ' && Background == ConsoleColor.Black && Foreground == ConsoleColor.Gray;
            }
        }

        public void Import(ColoredChar other, ConsoleColor background)
        {
            if (other == null)
            {
                return;
            }

            Foreground = other.Foreground;
            Char = other.Char;
            Background = other.Background;

            if (Background == ConsoleColor.Black)
            {
                if (other.Background == ConsoleColor.Black)
                {
                    Background = background;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null && IsNull)
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (!(obj is ColoredChar cc))
            {
                return false;
            }

            return cc.Char == Char && cc.Foreground == Foreground && cc.Background == Background;
        }

        public override int GetHashCode()
        {
            return 
                Background.GetHashCode() ^ 
                Foreground.GetHashCode() ^ 
                Char.GetHashCode();
        }
    }
}
