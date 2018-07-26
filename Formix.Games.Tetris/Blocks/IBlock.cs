namespace Formix.Games.Tetris
{
    public interface IBlock
    {
        int Position { get; }
        int Row { get; }
        int Col { get; }

        void Rotate();
        void RotateBack();
        void MoveLeft();
        void MoveRight();
        void MoveDown();
        void MoveUp();
        void Undo();
    }
}