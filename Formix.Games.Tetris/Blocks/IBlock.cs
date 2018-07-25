namespace Formix.Games.Tetris
{
    public interface IBlock
    {
        int Id { get; }
        int Position { get; }
        //int Speed { get; }

        void Rotate();
        void RotateBack();
        void MoveLeft();
        void MoveRight();
        void MoveDown();
    }
}