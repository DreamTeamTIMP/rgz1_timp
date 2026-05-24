namespace rgz1_timp.Command
{
    /// <summary>
    /// Статический класс, управляющий историей выполненных команд.
    /// Обеспечивает выполнение, отмену (Undo) и повтор (Redo) команд.
    /// </summary>
    public static class CommandInvoker
    {
        // Стек выполненных команд (для Undo).
        private static Stack<ICommand> undoStack = new Stack<ICommand>();

        // Стек отменённых команд (для Redo).
        private static Stack<ICommand> redoStack = new Stack<ICommand>();

        /// <summary>
        /// Выполняет переданную команду, помещает её в стек Undo и очищает стек Redo.
        /// </summary>
        /// <param name="command">Выполняемая команда.</param>
        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();                     
            undoStack.Push(command);
            redoStack.Clear();                     
        }

        /// <summary>
        /// Отменяет последнюю выполненную команду (если есть).
        /// Перемещает её из стека Undo в стек Redo.
        /// </summary>
        public static void Undo()
        {
            if (undoStack.Count > 0)
            {
                ICommand cmd = undoStack.Pop();    
                cmd.Undo();                        
                redoStack.Push(cmd);               
            }
        }

        /// <summary>
        /// Повторяет последнюю отменённую команду (если есть).
        /// Перемещает её из стека Redo обратно в стек Undo.
        /// </summary>
        public static void Redo()
        {
            if (redoStack.Count > 0)
            {
                ICommand cmd = redoStack.Pop();    
                cmd.Execute();                     
                undoStack.Push(cmd);               
            }
        }

        /// <summary>
        /// Полностью очищает историю команд (Undo и Redo).
        /// </summary>
        public static void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
        }

        /// <summary>
        /// Возвращает true, если есть хотя бы одна команда для отмены (Undo).
        /// </summary>
        public static bool CanUndo => undoStack.Count > 0;

        /// <summary>
        /// Возвращает true, если есть хотя бы одна команда для повтора (Redo).
        /// </summary>
        public static bool CanRedo => redoStack.Count > 0;
    }
}