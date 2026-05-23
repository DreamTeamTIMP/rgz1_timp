namespace rgz1_timp.Command
{

    public abstract class FileSystemCommand : ICommand
    {
        protected string sourcePath;
        protected string? backupPath; // для временного хранения при удалении/перемещении

        protected FileSystemCommand(string sourcePath)
        {
            this.sourcePath = sourcePath;
        }

        public abstract void Undo();
        public abstract void Execute();
    }
}