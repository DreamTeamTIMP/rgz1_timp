namespace rgz1_timp.Command
{
    public static class CommandInvoker
    {
        private static Stack<ICommand> undoStack = new Stack<ICommand>();
        private static Stack<ICommand> redoStack = new Stack<ICommand>();

        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear(); // новая команда стирает Redo
        }

        public static void Undo()
        {
            if (undoStack.Count > 0)
            {
                ICommand cmd = undoStack.Pop();
                cmd.Undo();
                redoStack.Push(cmd);
            }
        }

        public static void Redo()
        {
            if (redoStack.Count > 0)
            {
                ICommand cmd = redoStack.Pop();
                cmd.Execute();
                undoStack.Push(cmd);
            }
        }

        public static void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
        }
        public static bool CanUndo => undoStack.Count > 0;
        public static bool CanRedo => redoStack.Count > 0;
    }
}