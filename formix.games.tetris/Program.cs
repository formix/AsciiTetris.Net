using Formix.Games.Tetris.Display;
using System;
using System.Threading;

namespace Formix.Games.Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunTest();

            var mainFrame = new Frame("main", 22, 22)
            {
                Row = 1,
                Col = 27
            };


            IBlock block = new Pillar();
            mainFrame.Add(block);


            var screen = new Screen();
            screen.Canvases.Add(mainFrame);

            var loop = true;
            while (loop)
            {
                screen.Project();
                var keyInfo = Console.ReadKey(true);
                switch (keyInfo.KeyChar)
                {
                    case 'w': block.Rotate(); break;
                    case 's': block.MoveDown(); break;
                    case 'a': block.MoveLeft(); break;
                    case 'd': block.MoveRight(); break;
                    case 'r': screen.Refresh(); break;
                    case 'q': loop = false; break;
                }
            }
        }




        static void RunTest()
        {
            var sprite = new Sprite(4, 6)
            {
                Row = 1,
                Col = 2,
                ZIndex = 0
            };

            sprite.PrintH(0, 0, '+', 6, ConsoleColor.Yellow);
            sprite.PrintH(1, 0, '+', 6, ConsoleColor.Green);
            sprite.PrintH(2, 0, '+', 6, ConsoleColor.Blue);
            sprite.PrintH(3, 0, '+', 6, ConsoleColor.DarkBlue);

            var sprite2 = new Sprite(8, 8)
            {
                Row = 2,
                Col = 4,
                ZIndex = 1
            };
            for (int r = 0; r < 8; r++)
            {
                sprite2.PrintH(r, r, '0', 8 - r, ConsoleColor.White, ConsoleColor.DarkGray);
            }

            var canvas = new Canvas(12, 20)
            {
                Background = ConsoleColor.Red
            };
            canvas.Sprites.Add(sprite);
            canvas.Sprites.Add(sprite2);

            var screen = new Screen();
            screen.Canvases.Add(canvas);

            var canvas2 = new Canvas(1, 40)
            {
                ZIndex = 100,
                Row = 28
            };

            var statusSprite = new Sprite(1, 40);
            statusSprite.PrintH(0, 0, "Status bar:", ConsoleColor.Black);
            canvas2.Background = ConsoleColor.White;
            canvas2.Sprites.Add(statusSprite);

            screen.Canvases.Add(canvas2);


            screen.Project();


            int direction = 1;
            Timer t = new Timer((s) =>
            {
                if (direction < 0)
                {
                    sprite.ZIndex = 0;
                }
                else
                {
                    sprite.ZIndex = 2;
                }
                canvas.Refresh();
                sprite.Row += direction;
                sprite.Col += direction * 2;
                statusSprite.PrintH(0, 0, $"Status bar: ZIndex={sprite.ZIndex},{sprite2.ZIndex}", ConsoleColor.Black);
                direction = direction * -1;
                screen.Project();
            }, null, 1500, 1500);



            var loop = true;
            while (loop)
            {
                screen.Project();
                var keyInfo = Console.ReadKey(true);
                switch (keyInfo.KeyChar)
                {
                    case 'w': canvas.Row--; break;
                    case 's': canvas.Row++; break;
                    case 'a': canvas.Col--; break;
                    case 'd': canvas.Col++; break;
                    case 'r': screen.Refresh(); break;
                    case 'q': loop = false; break;
                }
            }

            t.Dispose();
        }
    }
}
