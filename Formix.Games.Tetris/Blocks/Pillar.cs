using Formix.Games.Tetris.Display;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Formix.Games.Tetris
{
    public class Pillar : Sprite, IBlock
    {
        //private Timer _timer;

        public int Id => 0;
        public int Position { get; private set; }
        //public int Speed { get; private set; }

        public Pillar() : base(4, 8)
        {
            Row = 1;
            Col = 9;
            Position = new Random().Next(2);
            UpdateDisplay();
            //Speed = -1;
            //_timer = null;
        }


        //public void Start(int speed)
        //{
        //    if (speed < 0 || speed > 20)
        //    {
        //        throw new ArgumentException(
        //            $"{nameof(speed)} must be between 0 and 20.", nameof(speed));
        //    }

        //    int delay = (int)(1250 * (1 - (speed * 0.05)) + 250);
        //    _timer = new Timer((s) => MoveDown(), null, delay, delay);
        //}

        //public void Stop()
        //{
        //    if (_timer != null)
        //    {
        //        _timer.Dispose();
        //        _timer = null;
        //        Speed = -1;
        //    }
        //}

        public void MoveLeft()
        {
            Col -= 2;
        }

        public void MoveRight()
        {
            Col += 2;
        }

        public void MoveDown()
        {
            Row++;
        }

        public void Rotate()
        {
            Position = (Position + 1) % 4;
            UpdateDisplay();
        }

        public void RotateBack()
        {
            Position = (Position + 3) % 4;
        }

        private void UpdateDisplay()
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
