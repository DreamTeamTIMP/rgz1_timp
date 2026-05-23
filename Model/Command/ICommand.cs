namespace rgz1_timp.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}