namespace rgz1_timp.Command
{
    /// <summary>
    /// Интерфейс, реализующий паттерн «Команда».
    /// Позволяет выполнять и отменять операции над файловой системой.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Выполняет команду.
        /// </summary>
        void Execute();

        /// <summary>
        /// Отменяет выполненную команду.
        /// </summary>
        void Undo();
    }
}