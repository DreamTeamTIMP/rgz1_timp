namespace rgz1_timp.Command
{
    public class NewFolderCommand : FileSystemCommand
    {
        private readonly string parentPath;
        private string folderName;
        private string fullPath;

        public NewFolderCommand(string parentPath, string folderName) : base(parentPath)
        {
            this.parentPath = parentPath;
            this.folderName = folderName;
            fullPath = Path.Combine(parentPath, folderName);
        }

        public override void Execute()
        {
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
        }

        public override void Undo()
        {
            if (Directory.Exists(fullPath))
                Directory.Delete(fullPath);
        }

    }
}