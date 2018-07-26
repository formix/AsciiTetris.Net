using Formix.Games.Tetris.Display;
using System;

namespace Formix.Games.Tetris.Blocks
{
    public abstract class AbstractBlock : Sprite, IBlock
    {
        private Actions _lastAction;

        public int Position { get; protected set; }

        public AbstractBlock(int height, int width) : base(height, width)
        {
            Position = new Random().Next(4);
            _lastAction = Actions.Unknown;
            UpdateDisplay();
        }

        public void MoveLeft()
        {
            if (Col > 1)
            {
                Col -= 2;
                _lastAction = Actions.MoveLeft;
            }
        }

        public void MoveRight()
        {
            if (Col < 19)
            {
                Col += 2;
                _lastAction = Actions.MoveRight;
            }
        }

        public void MoveDown()
        {
            Row++;
            _lastAction = Actions.MoveDown;
        }

        public void MoveUp()
        {
            Row--;
            _lastAction = Actions.MoveUp;

        }

        public void Rotate()
        {
            Position = (Position + 1) % 4;
            UpdateDisplay();
            _lastAction = Actions.RotateBack;
        }

        public void RotateBack()
        {
            Position = (Position + 3) % 4;
            UpdateDisplay();
            _lastAction = Actions.RotateBack;
        }

        public void Undo()
        {
            switch (_lastAction)
            {
                case Actions.Unknown:
                    throw new InvalidOperationException("Last action is unknown.");

                case Actions.MoveLeft:
                    MoveRight();
                    break;

                case Actions.MoveRight:
                    MoveLeft();
                    break;

                case Actions.MoveDown:
                    MoveUp();
                    break;

                case Actions.RotateBack:
                    RotateBack();
                    break;
            }

            _lastAction = Actions.Unknown;
            UpdateDisplay();
        }

        protected abstract void UpdateDisplay();
    }
}
