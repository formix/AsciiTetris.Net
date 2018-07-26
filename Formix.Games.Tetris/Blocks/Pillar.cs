using Formix.Games.Tetris.Display;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Formix.Games.Tetris.Blocks
{
    public class Pillar : AbstractBlock
    {
        public Pillar() : base(4, 8)
        {
            Row = 1;
            Col = 9;
        }

        protected override void UpdateDisplay()
        {
            Clear();
            switch (Position)
            {
                case 0:
                case 2:
                    PrintH(0, 0, "████████", ConsoleColor.Cyan);
                    break;

                case 1:
                case 3:
                    PrintV(0, 0, '█', 4, ConsoleColor.Cyan);
                    PrintV(0, 1, '█', 4, ConsoleColor.Cyan);
                    break;
            }
        }
    }
}
