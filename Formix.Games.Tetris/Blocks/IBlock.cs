namespace Formix.Games.Tetris
{
    public interface IBlock
    {
        int Position { get; }

        void Rotate();
        void RotateBack();
        void MoveLeft();
        void MoveRight();
        void MoveDown();
        void MoveUp();
        void Undo();
    }
}