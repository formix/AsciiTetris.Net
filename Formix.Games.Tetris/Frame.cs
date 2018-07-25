using Formix.Games.Tetris.Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formix.Games.Tetris
{
    public class Frame : Canvas
    {
        private Sprite _borders;

        public string Name { get; private set; }

        public Frame(string name, int height, int width, ConsoleColor background = ConsoleColor.Black) 
            : base(height, width, background)
        {
            _borders = CreateBorders();
            Sprites.Add(_borders);
            Name = name;
        }

        public Frame Add(IBlock block)
        {
            Sprites.Add((Sprite)block);
            return this;
        }

        private Sprite CreateBorders()
        {
            var sprite = new Sprite(Height, Width, Name);
            sprite.PrintH(0, 0, '╔', 1, ConsoleColor.White);
            sprite.PrintH(Height - 1, 0, '╚', 1, ConsoleColor.White);
            sprite.PrintH(0, Width - 1, '╗', 1, ConsoleColor.White);
            sprite.PrintH(Height - 1, Width - 1, '╝', 1, ConsoleColor.White);

            sprite.PrintH(0, 1, '═', Width - 2, ConsoleColor.White);
            sprite.PrintV(1, 0, '║', Height - 2, ConsoleColor.White);
            sprite.PrintH(Height - 1, 1, '═', Width - 2, ConsoleColor.White);
            sprite.PrintV(1, Width - 1, '║', Height - 2, ConsoleColor.White);

            return sprite;
        }
    }
}
