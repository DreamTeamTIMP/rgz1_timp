namespace rgz1_timp.Command
{
    public class RenameCommand : FileSystemCommand
    {
        private readonly string newName;
        private string oldName;
        private string parentDir;
        private string newPath;

        public RenameCommand(string oldFullPath, string newName) : base(oldFullPath)
        {
            this.newName = newName;
            oldName = Path.GetFileName(oldFullPath);
            parentDir = Path.GetDirectoryName(oldFullPath)!;
            newPath = Path.Combine(parentDir, newName);
        }

        public override void Execute()
        {
            if (Directory.Exists(sourcePath))
                Directory.Move(sourcePath, newPath);
            else if (File.Exists(sourcePath))
                File.Move(sourcePath, newPath);
        }

        public override void Undo()
        {
            if (Directory.Exists(newPath))
                Directory.Move(newPath, sourcePath);
            else if (File.Exists(newPath))
                File.Move(newPath, sourcePath);
        }
    }
}